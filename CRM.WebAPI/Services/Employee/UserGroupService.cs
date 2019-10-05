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
using UserGroup = CRM.DAL.Database.Entities.Employee.UserGroup;
namespace CRM.WebAPI.Services.Employee
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserGroupService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserGroupDto>> GetAsync()
        {
            var list = await _repository.Get<UserGroup>().ToListAsync();
            return _mapper.Map<IList<UserGroupDto>>(list);
        }

        public async Task<bool> UserGroupExistsAsync(int id)
        {
            return await _repository.Get<UserGroup>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserGroupDto> GetAllAsync(ODataQueryOptions<UserGroup> options)
        {
                var items = _repository.Get<UserGroup>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserGroup>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserGroupDto>>(items);
                return new PageResult<UserGroupDto>(result, null, count);
        }

        public async Task<UserGroupDto> GetAsync(int id)
        {
            var userGroup = await _repository.GetAsync<UserGroup>(id);

            return  _mapper.Map<UserGroupDto>(userGroup);
        }

        public async Task<int> SaveAsync(UserGroupDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserGroupDto entityDto)
        {
            var entity = _mapper.Map<UserGroup>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userGroup = await _repository.GetAsync<UserGroup>(id);

            if (userGroup == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userGroup);
            return result;
        }
    }
}
