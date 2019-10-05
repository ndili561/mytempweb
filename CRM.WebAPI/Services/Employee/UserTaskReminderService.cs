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
    public class UserTaskReminderService : IUserTaskReminderService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserTaskReminderService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTaskReminderDto>> GetAsync()
        {
            var list = await _repository.Get<UserTaskReminder>().ToListAsync();

            return _mapper.Map<IList<UserTaskReminderDto>>(list);
        }

        public async Task<bool> UserCalendarTaskReminderExistsAsync(int id)
        {
            return await _repository.Get<UserTaskReminder>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserTaskReminderDto> GetAllAsync(ODataQueryOptions<UserTaskReminder> options)
        {
                var items = _repository.Get<UserTaskReminder>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserTaskReminder>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserTaskReminderDto>>(items);
                return new PageResult<UserTaskReminderDto>(result, null, count);
        }

        public async Task<UserTaskReminderDto> GetAsync(int id)
        {
            var userCalendarTaskReminder = await _repository.GetAsync<UserTaskReminder>(id);

            return  _mapper.Map<UserTaskReminderDto>(userCalendarTaskReminder);
        }

        public async Task<int> SaveAsync(UserTaskReminderDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserTaskReminderDto entityDto)
        {
            var entity = _mapper.Map<UserTaskReminder>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userCalendarTaskReminder = await _repository.GetAsync<UserTaskReminder>(id);

            if (userCalendarTaskReminder == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userCalendarTaskReminder);

            return result;
        }
    }
}
