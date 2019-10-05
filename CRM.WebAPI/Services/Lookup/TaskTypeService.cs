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
    public class TaskTypeService : ITaskTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TaskTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskTypeDto>> GetAsync()
        {
            var list = await _repository.Get<TaskType>().ToListAsync();
            return _mapper.Map<IList<TaskTypeDto>>(list);
        }

        public async Task<bool> TaskTypeExistsAsync(int id)
        {
            return await _repository.Get<TaskType>().AnyAsync(p => p.Id == id);
        }

        public async Task<bool> TaskNotificationExistsAsync(int id)
        {
            return await _repository.Get<TaskType>().AnyAsync(p => p.Id == id);
        }

        public PageResult<TaskTypeDto> GetAllAsync(ODataQueryOptions<TaskType> options)
        {
                var items = _repository.Get<TaskType>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<TaskType>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<TaskTypeDto>>(items);
                return new PageResult<TaskTypeDto>(result, null, count);
        }

        public async Task<TaskTypeDto> GetAsync(int id)
        {
            var taskType = await _repository.GetAsync<TaskType>(id);

            return _mapper.Map<TaskTypeDto>(taskType);
        }

        public async Task<int> SaveAsync(TaskTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TaskTypeDto entityDto)
        {
            var entity = _mapper.Map<TaskType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var taskType = await _repository.GetAsync<TaskType>(id);

            if (taskType == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(taskType);

            return result;
        }
    }
}
