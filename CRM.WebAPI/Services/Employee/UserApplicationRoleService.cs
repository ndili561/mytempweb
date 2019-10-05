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
using UserApplicationRole = CRM.DAL.Database.Entities.Employee.UserApplicationRole;
namespace CRM.WebAPI.Services.Employee
{
    public class UserApplicationRoleService : IUserApplicationRoleService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserApplicationRoleService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserApplicationRoleDto>> GetAsync()
        {
            var list =await _repository.Get<UserApplicationRole>().ToListAsync();
            return _mapper.Map<IList<UserApplicationRoleDto>>(list);
        }

        public async Task<bool> UserApplicationRoleExistsAsync(int id)
        {
            return await _repository.Get<UserApplicationRole>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserApplicationRoleDto> GetAllAsync(ODataQueryOptions<UserApplicationRole> options)
        {
                var items = _repository.Get<UserApplicationRole>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserApplicationRole>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserApplicationRoleDto>>(items);
                return new PageResult<UserApplicationRoleDto>(result, null, count);
        }

        public async Task<UserApplicationRoleDto> GetAsync(int id)
        {
            var userApplicationRole = await _repository.GetAsync<UserApplicationRole>(id);

            return _mapper.Map<UserApplicationRoleDto>(userApplicationRole);
        }

        public async Task<int> SaveAsync(UserApplicationRoleDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserApplicationRoleDto entityDto)
        {
            var entity = _mapper.Map<UserApplicationRole>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userApplicationRole = await _repository.GetAsync<UserApplicationRole>(id);

            if (userApplicationRole == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userApplicationRole);

            return result;
        }
    }
}
