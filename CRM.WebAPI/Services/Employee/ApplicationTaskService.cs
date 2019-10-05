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
using ApplicationTask = CRM.DAL.Database.Entities.Employee.ApplicationTask;
namespace CRM.WebAPI.Services.Employee
{
    public class ApplicationTaskService : IApplicationTaskService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ApplicationTaskService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationTaskDto>> GetAsync()
        {
            var list = await _repository.Get<ApplicationTask>().ToListAsync();
            return _mapper.Map<IList<ApplicationTaskDto>>(list);
        }

        public async Task<bool> ApplicationTaskExistsAsync(int id)
        {
            return await _repository.Get<ApplicationTask>().AnyAsync(p => p.Id == id);
        }

        public PageResult<ApplicationTaskDto> GetAllAsync(ODataQueryOptions<ApplicationTask> options)
        {
                var items = _repository.Get<ApplicationTask>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<ApplicationTask>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<ApplicationTaskDto>>(items);
                return new PageResult<ApplicationTaskDto>(result, null, count);
        }

        public async Task<ApplicationTaskDto> GetAsync(int id)
        {
            var applicationTaskType = await _repository.GetAsync<ApplicationTask>(id);

            return _mapper.Map<ApplicationTaskDto>(applicationTaskType);
        }

        public async Task<int> SaveAsync(ApplicationTaskDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(ApplicationTaskDto entityDto)
        {
            var entity = _mapper.Map<ApplicationTask>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var applicationTaskType = await _repository.GetAsync<ApplicationTask>(id);

            if (applicationTaskType == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(applicationTaskType);

            return result;
        }
    }
}
