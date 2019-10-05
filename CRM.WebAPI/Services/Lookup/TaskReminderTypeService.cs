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
    public class TaskReminderTypeService : ITaskReminderTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TaskReminderTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskReminderTypeDto>> GetAsync()
        {
            var list = await _repository.Get<TaskReminderType>().ToListAsync();
            return _mapper.Map<IList<TaskReminderTypeDto>>(list);
        }

        public async Task<bool> TaskReminderTypeExistsAsync(int id)
        {
            return await _repository.Get<TaskReminderType>().AnyAsync(p => p.Id == id);
        }

        public async Task<bool> TaskReminderExistsAsync(int id)
        {
            return await _repository.Get<TaskReminderType>().AnyAsync(p => p.Id == id);
        }

        public PageResult<TaskReminderTypeDto> GetAllAsync(ODataQueryOptions<TaskReminderType> options)
        {
                var items = _repository.Get<TaskReminderType>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<TaskReminderType>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<TaskReminderTypeDto>>(items);
                return new PageResult<TaskReminderTypeDto>(result, null, count);
        }

        public async Task<TaskReminderTypeDto> GetAsync(int id)
        {
            var taskReminderType = await _repository.GetAsync<TaskReminderType>(id);

            return  _mapper.Map<TaskReminderTypeDto>(taskReminderType);
        }

        public async Task<int> SaveAsync(TaskReminderTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TaskReminderTypeDto entityDto)
        {
            var entity = _mapper.Map<TaskReminderType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var taskReminderType = await _repository.GetAsync<TaskReminderType>(id);

            if (taskReminderType == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(taskReminderType);

            return result;
        }
    }
}
