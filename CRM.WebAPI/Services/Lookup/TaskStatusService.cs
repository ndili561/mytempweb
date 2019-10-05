using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using TaskStatus = CRM.DAL.Database.Entities.Lookup.TaskStatus;

namespace CRM.WebAPI.Services.Lookup
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TaskStatusService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskStatusDto>> GetAsync()
        {
            var list = await _repository.Get<TaskStatus>().ToListAsync();
            return _mapper.Map<IList<TaskStatusDto>>(list);
        }

        public async Task<bool> TaskStatusExistsAsync(int id)
        {
            return await _repository.Get<TaskStatus>().AnyAsync(p => p.Id == id);
        }

        public async Task<bool> TaskNotificationExistsAsync(int id)
        {
            return await _repository.Get<TaskStatus>().AnyAsync(p => p.Id == id);
        }

        public PageResult<TaskStatusDto> GetAllAsync(ODataQueryOptions<TaskStatus> options)
        {
                var items = _repository.Get<TaskStatus>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<TaskStatus>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<TaskStatusDto>>(items);
                return new PageResult<TaskStatusDto>(result, null, count);
        }

        public async Task<TaskStatusDto> GetAsync(int id)
        {
            var taskStatus = await _repository.GetAsync<TaskStatus>(id);

            return _mapper.Map<TaskStatusDto>(taskStatus);
        }

        public async Task<int> SaveAsync(TaskStatusDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TaskStatusDto entityDto)
        {
            var entity = _mapper.Map<TaskStatus>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var taskStatus = await _repository.GetAsync<TaskStatus>(id);

            if (taskStatus == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(taskStatus);

            return result;
        }
    }
}
