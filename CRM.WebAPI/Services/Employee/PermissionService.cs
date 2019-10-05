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

namespace CRM.WebAPI.Services.Employee
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PermissionService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> GetAsync()
        {
            var list = await _repository.Get<Permission>().ToListAsync();
            return _mapper.Map<IList<PermissionDto>>(list);
        }

        public async Task<bool> PermissionExistsAsync(int id)
        {
            return await _repository.Get<Permission>().AnyAsync(p => p.Id == id);
        }

        public PageResult<PermissionDto> GetAllAsync(ODataQueryOptions<Permission> options)
        {
                var items = _repository.Get<Permission>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Permission>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<PermissionDto>>(items);
                return new PageResult<PermissionDto>(result, null, count);
        }

        public async Task<PermissionDto> GetAsync(int id)
        {
            var permission = await _repository.GetAsync<Permission>(id);

            return  _mapper.Map<PermissionDto>(permission);
        }

        public async Task<int> SaveAsync(PermissionDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PermissionDto entityDto)
        {
            var addressDb = _mapper.Map<Permission>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(addressDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permission = await _repository.GetAsync<Permission>(id);

            if (permission == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(permission);

            return result;
        }
    }
}
