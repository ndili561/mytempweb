using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Repository;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using UserActivity = CRM.DAL.Database.Entities.Employee.UserActivity;
namespace CRM.WebAPI.Services.Employee
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserActivityService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserActivityDto>> GetAsync()
        {
            var list = await _repository.Get<UserActivity>().ToListAsync();
            return _mapper.Map<IList<UserActivityDto>>(list);
        }

        public async Task<bool> UserActivityExistsAsync(int id)
        {
            return await _repository.Get<UserActivity>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserActivityDto> GetAllAsync(ODataQueryOptions<UserActivity> options)
        {
                var items = _repository.Get<UserActivity>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserActivity>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserActivityDto>>(items);
                return new PageResult<UserActivityDto>(result, null, count);
        }

        public async Task<UserActivityDto> GetAsync(int id)
        {
            var userActivity = await _repository.GetAsync<UserActivity>(id);
            return _mapper.Map<UserActivityDto>(userActivity);
        }

        public async Task<int> SaveAsync(UserActivityDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserActivityDto entityDto)
        {
            var entity = _mapper.Map<UserActivity>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userActivity = await _repository.GetAsync<UserActivity>(id);

            if (userActivity == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userActivity);

            return result;
        }
    }
}
