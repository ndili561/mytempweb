using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using Address = CRM.DAL.Database.Entities.Customer.Address;

namespace CRM.WebAPI.Services.Lookup
{
    public class AddressTypeService : IAddressTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AddressTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressTypeDto>> GetAsync()
        {
            var list = await _repository.Get<AddressType>().ToListAsync();
            return _mapper.Map<IList<AddressTypeDto>>(list);
        }

        public async Task<bool> AddressTypeExistsAsync(int id)
        {
            return await _repository.Get<AddressType>().AnyAsync(p => p.Id == id);
        }

        public async Task<bool> AddressExistsAsync(int id)
        {
            return await _repository.Get<Address>().AnyAsync(p => p.Id == id);
        }

       
        public async Task<AddressTypeDto> GetAsync(int id)
        {
            var address = await _repository.GetAsync<AddressType>(id);

            return  _mapper.Map<AddressTypeDto>(address);
        }

        public async Task<int> SaveAsync(AddressTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AddressTypeDto entityDto)
        {
            var entity = _mapper.Map<AddressType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var address = await _repository.GetAsync<AddressType>(id);

            if (address == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(address);

            return result;
        }
    }
}
