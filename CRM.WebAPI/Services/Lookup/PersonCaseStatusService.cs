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
    public class PersonCaseStatusService : IPersonCaseStatusService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonCaseStatusService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonCaseStatusDto>> GetAsync()
        {
            var list = await _repository.Get<PersonCaseStatus>().ToListAsync();
            return _mapper.Map<IList<PersonCaseStatusDto>>(list);
        }

        public async Task<bool> CaseStatusExistsAsync(int id)
        {
            return await _repository.Get<PersonCaseStatus>().AnyAsync(p => p.Id == id);
        }

        public async Task<PersonCaseStatusDto> GetAsync(int id)
        {
            var Case = await _repository.GetAsync<PersonCaseStatus>(id);

            return  _mapper.Map<PersonCaseStatusDto>(Case);
        }

        public async Task<int> SaveAsync(PersonCaseStatusDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonCaseStatusDto entityDto)
        {
            var entity = _mapper.Map<PersonCaseStatus>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Case = await _repository.GetAsync<PersonCaseStatus>(id);

            if (Case == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(Case);

            return result;
        }
    }
}
