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
    public class PersonCaseTypeService : IPersonCaseTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonCaseTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonCaseTypeDto>> GetAsync()
        {
            var list = await _repository.Get<PersonCaseType>().ToListAsync();
            return _mapper.Map<IList<PersonCaseTypeDto>>(list);
        }

        public async Task<bool> CaseTypeExistsAsync(int id)
        {
            return await _repository.Get<PersonCaseType>().AnyAsync(p => p.Id == id);
        }

        public async Task<PersonCaseTypeDto> GetAsync(int id)
        {
            var Case = await _repository.GetAsync<PersonCaseType>(id);

            return  _mapper.Map<PersonCaseTypeDto>(Case);
        }

        public async Task<int> SaveAsync(PersonCaseTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonCaseTypeDto entityDto)
        {
            var entity = _mapper.Map<PersonCaseType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Case = await _repository.GetAsync<PersonCaseType>(id);

            if (Case == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(Case);

            return result;
        }
    }
}
