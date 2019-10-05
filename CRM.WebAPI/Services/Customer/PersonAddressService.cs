using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonAddressService : IPersonAddressService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonAddressService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonAddressDto>> GetAsync()
        {
            var list = await _repository.Get<PersonAddress>().ToListAsync();
            return _mapper.Map<IList<PersonAddressDto>>(list);
        }

        public async Task<bool> PersonAddressExistsAsync(int id)
        {
            return await _repository.Get<PersonAddress>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonAddressDto> GetAsync(int id)
        {
            var personAddress = await _repository.GetAsync<PersonAddress>(id);

            return _mapper.Map<PersonAddressDto>(personAddress);
        }

        public async Task<int> SaveAsync(PersonAddressDto personAddress)
        {
            var result = await SaveAndReturnEntityAsync(personAddress);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonAddressDto entityDto)
        {
            var personAddress = _mapper.Map<PersonAddress>(entityDto);
            var address = _mapper.Map<Address>(personAddress.Address);
            address = await _repository.SaveAndReturnEntityAsync(address);
            if (personAddress.AddressId == 0)
            {
             
                personAddress.Address = null;
                personAddress.AddressId = address.Id;
                personAddress = await _repository.SaveAndReturnEntityAsync(personAddress);
            }
            personAddress.SuccessMessage = "The data is saved successfully";
            return personAddress;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personAddress = await _repository.GetAsync<PersonAddress>(id);

            if (personAddress == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personAddress);

            return result;
        }
    }
}
