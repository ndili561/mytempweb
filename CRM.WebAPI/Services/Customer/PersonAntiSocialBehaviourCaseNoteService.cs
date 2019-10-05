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
    public class PersonAntiSocialBehaviourCaseNoteService : IPersonAntiSocialBehaviourCaseNoteService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonAntiSocialBehaviourCaseNoteService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonAntiSocialBehaviourCaseNoteDto>> GetAsync()
        {
            var list = await _repository.Get<PersonAntiSocialBehaviourCaseNote>().ToListAsync();
            return _mapper.Map<IList<PersonAntiSocialBehaviourCaseNoteDto>>(list);
        }

        public async Task<bool> PersonAntiSocialBehaviourCaseNoteExistsAsync(int id)
        {
            return await _repository.Get<PersonAntiSocialBehaviourCaseNote>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonAntiSocialBehaviourCaseNoteDto> GetAsync(int id)
        {
            var personAntiSocialBehaviour = await _repository.CRMContext.PersonAntiSocialBehaviourCaseNotes
                .Include(x => x.PersonAntiSocialBehaviour)
                .ThenInclude(a => a.Person)
                .Include(x => x.PersonAntiSocialBehaviour)
                .ThenInclude(a => a.CaseStatus)
                .Include(x => x.PersonAntiSocialBehaviour)
                .ThenInclude(a => a.CaseType)
                .FirstOrDefaultAsync(z => z.Id == id);

            return _mapper.Map<PersonAntiSocialBehaviourCaseNoteDto>(personAntiSocialBehaviour);
        }

        public async Task<int> SaveAsync(PersonAntiSocialBehaviourCaseNoteDto personAntiSocialBehaviour)
        {
            var result = await SaveAndReturnEntityAsync(personAntiSocialBehaviour);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonAntiSocialBehaviourCaseNoteDto entityDto)
        {
            var personAntiSocialBehaviour = _mapper.Map<PersonAntiSocialBehaviourCaseNote>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(personAntiSocialBehaviour);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personAntiSocialBehaviour = await _repository.GetAsync<PersonAntiSocialBehaviourCaseNote>(id);

            if (personAntiSocialBehaviour == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personAntiSocialBehaviour);

            return result;
        }
    }
}
