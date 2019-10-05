using System.Collections.Generic;
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
    public class UserPersonTaskService : IUserPersonTaskService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserPersonTaskService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserPersonTaskDto>> GetAsync()
        {
            var list = await _repository.Get<UserPersonTask>().ToListAsync();
            return _mapper.Map<IList<UserPersonTaskDto>>(list);
        }

        public async Task<bool> UserCalendarTaskExistsAsync(int id)
        {
            return await _repository.Get<UserPersonTask>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserPersonTaskDto> GetAllAsync(ODataQueryOptions<UserPersonTask> options)
        {
            var items = _repository.Get<UserPersonTask>().AsQueryable();
            var count = items.Count();
            if (options.OrderBy != null)
                items = options.OrderBy.ApplyTo(items);  //perform sort 
            if (options.Filter != null)
                items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserPersonTask>();  //perform filter 

            count = items.ToList().Count;


            if (options.Skip != null)
                items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
            if (options.Top != null)
                items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
            var result = _mapper.Map<List<UserPersonTaskDto>>(items);
            return new PageResult<UserPersonTaskDto>(result, null, count);
        }

        public async Task<UserPersonTaskDto> GetAsync(int id)
        {
            var userCalendarTask = await _repository.GetAsync<UserPersonTask>(id);

            return _mapper.Map<UserPersonTaskDto>(userCalendarTask);
        }

        public async Task<int> SaveAsync(UserPersonTaskDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserPersonTaskDto entityDto)
        {
            var entity = _mapper.Map<UserPersonTask>(entityDto);
            if (entityDto.Id > 0)
            {
                var persistedEntity = _repository.CRMContext.UserPersonTasks.AsNoTracking().FirstOrDefault(x => x.Id == entityDto.Id);
                if (persistedEntity != null)
                {
                    entity.TaskId = persistedEntity.TaskId;
                    entity.TaskTypeId = persistedEntity.TaskTypeId;
                    entity.RowVersion = persistedEntity.RowVersion;

                }
            }

            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userCalendarTask = await _repository.GetAsync<UserPersonTask>(id);

            if (userCalendarTask == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userCalendarTask);
            return result;
        }
    }
}
