using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Repository;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Employee
{
    public class UserTaskNotificationService : IUserTaskNotificationService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserTaskNotificationService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTaskNotificationDto>> GetAsync()
        {
            var list = await  _repository.Get<UserTaskNotification>().ToListAsync();
            return _mapper.Map<IList<UserTaskNotificationDto>>(list);
        }

        public async Task<bool> UserCalendarTaskNotificationExistsAsync(int id)
        {
            return await _repository.Get<UserTaskNotification>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserTaskNotificationDto> GetAllAsync(ODataQueryOptions<UserTaskNotification> options)
        {
                var items = _repository.Get<UserTaskNotification>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserTaskNotification>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserTaskNotificationDto>>(items);
                return new PageResult<UserTaskNotificationDto>(result, null, count);
        }

        public async Task<UserTaskNotificationDto> GetAsync(int id)
        {
            var userCalendarTaskNotification = await _repository.GetAsync<UserTaskNotification>(id);

            return  _mapper.Map<UserTaskNotificationDto>(userCalendarTaskNotification);
        }

        public async Task<int> SaveAsync(UserTaskNotificationDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserTaskNotificationDto entityDto)
        {
            var entity = _mapper.Map<UserTaskNotification>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userCalendarTaskNotification = await _repository.GetAsync<UserTaskNotification>(id);

            if (userCalendarTaskNotification == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userCalendarTaskNotification);

            return result;
        }
    }
}
