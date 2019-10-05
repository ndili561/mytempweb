using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Lookup
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ContactTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactTypeDto>> GetAsync()
        {
            var list = await _repository.Get<ContactType>().ToListAsync();
            return _mapper.Map<IList<ContactTypeDto>>(list);
        }

        public async Task<bool> ContactTypeExistsAsync(int id)
        {
            return await _repository.Get<ContactType>().AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ContactExistsAsync(int id)
        {
            return await _repository.Get<ContactType>().AnyAsync(p => p.Id == id);
        }

      

        public async Task<ContactTypeDto> GetAsync(int id)
        {
            var contactType = await _repository.GetAsync<ContactType>(id);

            return  _mapper.Map<ContactTypeDto>(contactType);
        }

        public async Task<int> SaveAsync(ContactTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(ContactTypeDto entityDto)
        {
            var entity = _mapper.Map<ContactType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var address = await _repository.GetAsync<ContactType>(id);

            if (address == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(address);

            return result;
        }
    }
}
