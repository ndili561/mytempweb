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
    public class PersonFlagService : IPersonFlagService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonFlagService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonFlagDto>> GetAsync()
        {
            var list = await _repository.Get<PersonFlag>().ToListAsync();
            return _mapper.Map<IList<PersonFlagDto>>(list);
        }

        public async Task<bool> PersonFlagExistsAsync(int id)
        {
            return await _repository.Get<PersonFlag>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonFlagDto> GetAsync(int id)
        {
            var personFlag = await _repository.GetAsync<PersonFlag>(id);

            return _mapper.Map<PersonFlagDto>(personFlag);
        }

        public async Task<int> SaveAsync(PersonFlagDto personFlag)
        {
            var result = await SaveAndReturnEntityAsync(personFlag);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonFlagDto entityDto)
        {
            var personFlag = _mapper.Map<PersonFlag>(entityDto);

            if (entityDto.Id == 0)
            {
                var loggedUsed = _repository.CRMContext.IdentityUserView.FirstOrDefault(
                    x => x.Id == _repository.CRMContext.CurrentLoggedUserId);
                personFlag.CreatedBy = loggedUsed?.FirstName + " " + loggedUsed?.LastName;
                personFlag.CreatedOn = DateTime.Now;
            }
            var result = await _repository.SaveAndReturnEntityAsync(personFlag);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personFlag = await _repository.GetAsync<PersonFlag>(id);

            if (personFlag == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personFlag);

            return result;
        }
    }
}
