using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.EntityFrameworkCore;
using Tenant = CRM.DAL.Database.Entities.Customer.Tenant;

namespace CRM.WebAPI.Services.Customer
{
    public class TenantService : ITenantService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TenantService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> TenantExistsAsync(int id)
        {
            return await _repository.Get<Tenant>().AnyAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<TenantDto>> GetAsync()
        {
            var list = await _repository.Get<Tenant>().ToListAsync();
            return _mapper.Map<IList<TenantDto>>(list);
        }

        public async Task<TenantDto> GetAsync(int id)
        {
            var tenant = await _repository.GetAsync<Tenant>(id);

            return  _mapper.Map<TenantDto>(tenant);
        }

        public async Task<int> SaveAsync(TenantDto tenant)
        {
            var result = await SaveAndReturnEntityAsync(tenant);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TenantDto entityDto)
        {
            var tenantDb = _mapper.Map<Tenant>(entityDto);
            if (tenantDb.PropertyId ==0 )
            {
                var property = _repository.CRMContext.Properties.FirstOrDefaultAsync(x => x.PropertyCode == entityDto.PropertyCode);
                tenantDb.PropertyId = property.Id;
            }
            if (tenantDb.PersonId == 0)
            {
                var person = _repository.CRMContext.Persons.FirstOrDefaultAsync(x => x.GlobalIdentityKey == entityDto.PersonKey);
                tenantDb.PersonId = person.Id;
            }
            var result = await _repository.SaveAndReturnEntityAsync(tenantDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tenant = await _repository.GetAsync<Tenant>(id);
            if (tenant == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(tenant);
            return result;
        }
    }
}
