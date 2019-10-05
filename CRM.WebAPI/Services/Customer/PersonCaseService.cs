using System;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonCaseService : IPersonCaseService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonCaseService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonCaseDto>> GetAsync()
        {
            var list = await _repository.Get<PersonCase>().ToListAsync();
            return _mapper.Map<IList<PersonCaseDto>>(list);
        }

        public async Task<bool> PersonCaseExistsAsync(int id)
        {
            return await _repository.Get<PersonCase>().AnyAsync(p => p.Id == id);
        }

       

        public async Task<PersonCaseDto> GetAsync(int id)
        {
            var personCase = await _repository.GetAsync<PersonCase>(id);

            return _mapper.Map<PersonCaseDto>(personCase);
        }

        public async Task<int> SaveAsync(PersonCaseDto personCase)
        {
            var result = await SaveAndReturnEntityAsync(personCase);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonCaseDto entityDto)
        {
            var personCase = _mapper.Map<PersonCase>(entityDto);
            if (entityDto.Id == 0)
            {
                var loggedUsed = _repository.CRMContext.IdentityUserView.FirstOrDefault(
                    x => x.Id == _repository.CRMContext.CurrentLoggedUserId);
                personCase.CreatedBy = loggedUsed?.FirstName + " " + loggedUsed?.LastName;
                personCase.CreatedOn = DateTime.Now;
            }
            var result = await _repository.SaveAndReturnEntityAsync(personCase);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personCase = await _repository.GetAsync<PersonCase>(id);

            if (personCase == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personCase);

            return result;
        }
    }
}
