using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities;

namespace CRM.DAL.Repository
{
    public interface IRepository
    {
        CRMContext CRMContext { get; }
        void UpdateCurrentLoggedUserId(string userId,bool logAudit);
        IQueryable<T> Get<T>() where T : BaseEntity;
        IQueryable<T> GetWithNoTracking<T>() where T : BaseEntity;
        Task<T> GetAsync<T>(int id) where T : BaseEntity;
        Task<bool> HardDeleteAsync<T>(T obj) where T : BaseEntity;
        Task<bool> HardDeleteAsync<T>(ICollection<T> objs) where T : BaseEntity;
        Task<int> SaveAsync<T>(T obj) where T : BaseEntity;
        Task<T> SaveAndReturnEntityAsync<T>(T obj) where T : BaseEntity;
        Task<int> SaveAsync<T>(ICollection<T> objs) where T : BaseEntity;
        //Task<bool> DeleteAsync<T>(ICollection<T> objs) where T : BaseEntity;
        //Task<bool> DeleteAsync<T>(T obj) where T : BaseEntity;
    }
}
