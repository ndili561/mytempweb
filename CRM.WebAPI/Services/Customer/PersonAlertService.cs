using System;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonAlertService : IPersonAlertService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonAlertService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonAlertDto>> GetAsync()
        {
            var list = await _repository.Get<PersonAlert>().ToListAsync();
            return _mapper.Map<IList<PersonAlertDto>>(list);
        }

        public async Task<bool> PersonAlertExistsAsync(int id)
        {
            return await _repository.Get<PersonAlert>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonAlertDto> GetAsync(int id)
        {
            var personAlert = await _repository.CRMContext.PersonAlerts.Include(x=>x.Person).FirstOrDefaultAsync(x=>x.Id== id);
            return _mapper.Map<PersonAlertDto>(personAlert);
        }

        public async Task<int> SaveAsync(PersonAlertDto personAlert)
        {
            var result = await SaveAndReturnEntityAsync(personAlert);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonAlertDto entityDto)
        {
            var personAlert = _mapper.Map<PersonAlert>(entityDto);
            if (entityDto.Id == 0)
            {
                var loggedUsed = _repository.CRMContext.IdentityUserView.FirstOrDefault(
                    x => x.Id == _repository.CRMContext.CurrentLoggedUserId);
                personAlert.CreatedBy = loggedUsed?.FirstName + " " + loggedUsed?.LastName;
                personAlert.CreatedOn = DateTime.Now;
            }
            var result = await _repository.SaveAndReturnEntityAsync(personAlert);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personAlert = await _repository.GetAsync<PersonAlert>(id);

            if (personAlert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personAlert);

            return result;
        }
    }
}
