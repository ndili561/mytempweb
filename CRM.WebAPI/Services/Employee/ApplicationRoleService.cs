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
using ApplicationRole = CRM.DAL.Database.Entities.Employee.ApplicationRole;
namespace CRM.WebAPI.Services.Employee
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ApplicationRoleService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationRoleDto>> GetAsync()
        {
            var list = await _repository.Get<ApplicationRole>().ToListAsync();
            return _mapper.Map<IList<ApplicationRoleDto>>(list);
        }

        public async Task<bool> ApplicationRoleExistsAsync(int id)
        {
            return await _repository.Get<ApplicationRole>().AnyAsync(p => p.Id == id);
        }

        public PageResult<ApplicationRoleDto> GetAllAsync(ODataQueryOptions<ApplicationRole> options)
        {
                var items = _repository.Get<ApplicationRole>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<ApplicationRole>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<ApplicationRoleDto>>(items);
                return new PageResult<ApplicationRoleDto>(result, null, count);
        }

        public async Task<ApplicationRoleDto> GetAsync(int id)
        {
            var applicationRole = await _repository.GetAsync<ApplicationRole>(id);

            return _mapper.Map<ApplicationRoleDto>(applicationRole);
        }

        public async Task<int> SaveAsync(ApplicationRoleDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(ApplicationRoleDto entityDto)
        {
            var entity = _mapper.Map<ApplicationRole>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var applicationRole = await _repository.GetAsync<ApplicationRole>(id);

            if (applicationRole == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(applicationRole);

            return result;
        }
    }
}
