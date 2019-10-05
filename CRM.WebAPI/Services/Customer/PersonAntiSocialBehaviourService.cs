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
    public class PersonAntiSocialBehaviourService : IPersonAntiSocialBehaviourService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonAntiSocialBehaviourService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonAntiSocialBehaviourDto>> GetAsync()
        {
            var list = await _repository.Get<PersonAntiSocialBehaviour>().ToListAsync();
            return _mapper.Map<IList<PersonAntiSocialBehaviourDto>>(list);
        }

        public async Task<bool> PersonAntiSocialBehaviourExistsAsync(int id)
        {
            return await _repository.Get<PersonAntiSocialBehaviour>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonAntiSocialBehaviourDto> GetAsync(int id)
        {
            var personAntiSocialBehaviour = await _repository.CRMContext.PersonAntiSocialBehaviours
                .Include(x => x.Person)
                .Include(x => x.PersonAntiSocialBehaviourCaseNotes)
                .Include(a => a.CaseType)
                .Include(x => x.CaseStatus)
                .FirstOrDefaultAsync(z => z.Id == id);
            return _mapper.Map<PersonAntiSocialBehaviourDto>(personAntiSocialBehaviour);
        }

        public async Task<int> SaveAsync(PersonAntiSocialBehaviourDto personAntiSocialBehaviour)
        {
            var result = await SaveAndReturnEntityAsync(personAntiSocialBehaviour);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonAntiSocialBehaviourDto entityDto)
        {
            var personAntiSocialBehaviour = _mapper.Map<PersonAntiSocialBehaviour>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(personAntiSocialBehaviour);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personAntiSocialBehaviour = await _repository.GetAsync<PersonAntiSocialBehaviour>(id);

            if (personAntiSocialBehaviour == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personAntiSocialBehaviour);

            return result;
        }
    }
}
