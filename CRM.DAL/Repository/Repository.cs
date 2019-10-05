using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRM.DAL.Repository
{
    public class Repository<TDbContext> : IRepository where TDbContext : CRMContext
    {
        public CRMContext CRMContext
        {
            get => _crmContext;
        }
        private IDbContextTransaction _transaction;
        public string CurrentLoggedUserId;
        public bool LogAudit;
        private CRMContext _crmContext;

        public Repository(TDbContext context)
        {
            _crmContext = context;
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new NotImplementedException("Nested transaction is not supported.");
            }

            _transaction = _crmContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public bool IsTransactionInProgress()
        {
            return _transaction != null;
        }

        public void UpdateCurrentLoggedUserId(string userId, bool logAudit)
        {
            CurrentLoggedUserId = userId;
            LogAudit = logAudit;
        }



        public IQueryable<T> Get<T>() where T : BaseEntity
        {
            return _crmContext.Set<T>();
        }

        public IQueryable<T> GetWithNoTracking<T>() where T : BaseEntity
        {
            return _crmContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetAsync<T>(int id) where T : BaseEntity
        {

            return await _crmContext.Set<T>().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> HardDeleteAsync<T>(T obj) where T : BaseEntity
        {
            return await HardDeleteAsync(new[] { obj });
        }

        public async Task<bool> HardDeleteAsync<T>(ICollection<T> objs) where T : BaseEntity
        {
            return await HardDeleteAsync(objs.ToArray());
        }

        public async Task<int> SaveAsync<T>(T obj) where T : BaseEntity
        {
            return await SaveAsync(new[] { obj });
        }

        public async Task<T> SaveAndReturnEntityAsync<T>(T obj) where T : BaseEntity
        {
            try
            {
                var user = _crmContext.IdentityUserView.FirstOrDefault(x => x.Id == CurrentLoggedUserId|| x.Email == CurrentLoggedUserId);
                if (user == null )
                    throw new Exception($"User does not exist in the Identity Server.");
                _crmContext.CurrentLoggedUserId = user.Id;
                _crmContext.RecordCustomAudit = true;
                var dbSet = _crmContext.Set<T>();
                var ids = new List<int> { obj.Id };
                var objDbs = dbSet.Where(p => ids.Contains(obj.Id)).Select(p => new { p.Id, p.RowVersion })
                    .ToList();
                var objDb = objDbs.SingleOrDefault(p => p.Id == obj.Id);
                if (objDb != null && !objDb.RowVersion.SequenceEqual(obj.RowVersion))
                    throw new DBConcurrencyException(
                        $"Concurrency error -> object of type {typeof(T)} with id {obj.Id} was modified by someone else.");
                if (objDb == null)
                    await dbSet.AddAsync(obj);
                else
                    dbSet.Update(obj);
                await _crmContext.SaveChangesAsync();
                obj.SuccessMessage = "Saved action completed successfully.";
                return obj;
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                while (e != null)
                {
                    sb.AppendLine(e.Message);
                    e = e.InnerException;
                }
                obj.ErrorMessage = sb.ToString();
                return obj;
            }
        }

        public async Task<int> SaveAsync<T>(ICollection<T> objs) where T : BaseEntity
        {
            return await SaveAsync(objs.ToArray());
        }

        //public async Task<bool> DeleteAsync<T>(ICollection<T> objs) where T : BaseEntity
        //{
        //    return await DeleteAsync(objs.ToArray());
        //}

        //public async Task<bool> DeleteAsync<T>(T obj) where T : BaseEntity
        //{
        //    return await DeleteAsync(new[] { obj });
        //}
        private async Task<int> SaveAsync<T>(params T[] objs) where T : BaseEntity
        {
            T savedEntity = null;
            foreach (var obj in objs)
            {
                savedEntity = await SaveAndReturnEntityAsync(obj);
            }
            return savedEntity?.Id ?? 0;
        }

        //private async Task<bool> DeleteAsync<T>(params T[] objs) where T : BaseEntity
        //{

        //    var objToDelete = objs.ToArray();

        //    foreach (var obj in objToDelete)
        //        obj.IsDeleted = true;

        //    return await SaveAsync(objToDelete) == objToDelete.Length;
        //}

        private async Task<bool> HardDeleteAsync<T>(params T[] objs) where T : BaseEntity
        {
            var dbSet = _crmContext.Set<T>();
            dbSet.RemoveRange(objs);
            _crmContext.CurrentLoggedUserId = CurrentLoggedUserId;
            _crmContext.RecordCustomAudit = true;
            return await _crmContext.SaveChangesAsync() == objs.Length;
        }


    }
}