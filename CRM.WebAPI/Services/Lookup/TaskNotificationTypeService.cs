using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Lookup
{
    public class TaskNotificationTypeService : ITaskNotificationTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TaskNotificationTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskNotificationTypeDto>> GetAsync()
        {
            var list = await _repository.Get<TaskNotificationType>().ToListAsync();
            return _mapper.Map<IList<TaskNotificationTypeDto>>(list);
        }

        public async Task<bool> TaskNotificationTypeExistsAsync(int id)
        {
            return await _repository.Get<TaskNotificationType>().AnyAsync(p => p.Id == id);
        }

        public async Task<bool> TaskNotificationExistsAsync(int id)
        {
            return await _repository.Get<TaskNotificationType>().AnyAsync(p => p.Id == id);
        }

        public PageResult<TaskNotificationTypeDto> GetAllAsync(ODataQueryOptions<TaskNotificationType> options)
        {
                var items = _repository.Get<TaskNotificationType>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<TaskNotificationType>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<TaskNotificationTypeDto>>(items);
                return new PageResult<TaskNotificationTypeDto>(result, null, count);
        }

        public async Task<TaskNotificationTypeDto> GetAsync(int id)
        {
            var taskNotificationType = await _repository.GetAsync<TaskNotificationType>(id);

            return _mapper.Map<TaskNotificationTypeDto>(taskNotificationType);
        }

        public async Task<int> SaveAsync(TaskNotificationTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TaskNotificationTypeDto entityDto)
        {
            var entity = _mapper.Map<TaskNotificationType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var taskNotificationType = await _repository.GetAsync<TaskNotificationType>(id);

            if (taskNotificationType == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(taskNotificationType);

            return result;
        }
    }
}
