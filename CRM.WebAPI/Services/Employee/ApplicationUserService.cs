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
using ApplicationUser = CRM.DAL.Database.Entities.Employee.ApplicationUser;
namespace CRM.WebAPI.Services.Employee
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ApplicationUserService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAsync()
        {
            var list = await _repository.Get<ApplicationUser>().ToListAsync();
            return _mapper.Map<IList<ApplicationUserDto>>(list);
        }

        public async Task<bool> ApplicationUserExistsAsync(int id)
        {
            return await _repository.Get<ApplicationUser>().AnyAsync(p => p.Id == id);
        }

        public PageResult<ApplicationUserDto> GetAllAsync(ODataQueryOptions<ApplicationUser> options)
        {
                var items = _repository.Get<ApplicationUser>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<ApplicationUser>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<ApplicationUserDto>>(items);
                return new PageResult<ApplicationUserDto>(result, null, count);
        }

        public async Task<ApplicationUserDto> GetAsync(int id)
        {
            var applicationUser = await _repository.GetAsync<ApplicationUser>(id);

            return _mapper.Map<ApplicationUserDto>(applicationUser);
        }

        public async Task<int> SaveAsync(ApplicationUserDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(ApplicationUserDto entityDto)
        {
            var entity = _mapper.Map<ApplicationUser>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var applicationUser = await _repository.GetAsync<ApplicationUser>(id);

            if (applicationUser == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(applicationUser);

            return result;
        }
    }
}
