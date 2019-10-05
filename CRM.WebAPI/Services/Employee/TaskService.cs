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
using Task = CRM.DAL.Database.Entities.Employee.Task;

namespace CRM.WebAPI.Services.Employee
{
    public class TaskService : ITaskService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetAsync()
        {
            var list = await _repository.Get<Task>().ToListAsync();
            return _mapper.Map<IList<TaskDto>>(list);
        }

        public async Task<bool> TaskExistsAsync(int id)
        {
            return await _repository.Get<Task>().AnyAsync(p => p.Id == id);
        }

        public PageResult<TaskDto> GetAllAsync(ODataQueryOptions<Task> options)
        {
            var items = _repository.Get<Task>().AsQueryable();
            var count = items.Count();
            if (options.OrderBy != null)
                items = options.OrderBy.ApplyTo(items);  //perform sort 
            if (options.Filter != null)
                items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Task>();  //perform filter 

            count = items.ToList().Count;


            if (options.Skip != null)
                items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
            if (options.Top != null)
                items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
            var result = _mapper.Map<List<TaskDto>>(items);
            return new PageResult<TaskDto>(result, null, count);
        }

        public async Task<TaskDto> GetAsync(int id)
        {
            var task = await _repository.Get<Task>()
                .Include(x => x.ApplicationTasks)
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<int> SaveAsync(TaskDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TaskDto entityDto)
        {
            var entity = _mapper.Map<Task>(entityDto);

            if (!entityDto.UpdateLinkedApplication)
            {
                var result = await _repository.SaveAndReturnEntityAsync(entity);
                result.SuccessMessage = "The details updated successfully.";
                return result;
            }
            var persistedLinkedApplication = GetAsync(entityDto.Id).Result.ApplicationTasks.Select(x => x.ApplicationId).ToList();
            var currentLinkedApplication = entityDto.LinkedApplicationTaskIds?.Select(int.Parse).ToList();
            if (currentLinkedApplication != null)
            {
                var addedApplications = currentLinkedApplication.Where(x => !persistedLinkedApplication.Contains(x)).ToList();
                foreach (var addedApplication in addedApplications)
                {
                    var linkedApplication = new ApplicationTask { ApplicationId = addedApplication, TaskId = entityDto.Id };
                    await _repository.SaveAndReturnEntityAsync(linkedApplication);
                }
                var deletedApplications = persistedLinkedApplication.Where(x => !currentLinkedApplication.Contains(x)).ToList();
                foreach (var deletedApplication in deletedApplications)
                {
                    var applicationTask = await _repository.Get<ApplicationTask>()
                        .FirstOrDefaultAsync(x => x.TaskId == entityDto.Id && x.ApplicationId == deletedApplication);
                    await _repository.HardDeleteAsync(applicationTask);
                }
            }
            return new Task { Id= entity.Id, SuccessMessage = "The application linked updated successfully." };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var calendarTask = await _repository.GetAsync<Task>(id);

            if (calendarTask == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(calendarTask);

            return result;
        }
    }
}
