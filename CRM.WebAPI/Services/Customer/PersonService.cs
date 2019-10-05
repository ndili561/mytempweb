using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonService : IPersonService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> PersonExistsAsync(int id)
        {
            return await _repository.Get<Person>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonDto> GetAsyncByGlobalIdentityKey(Guid globalIdentityKey)
        {
            var person = await _repository.GetWithNoTracking<Person>().FirstOrDefaultAsync(x => x.GlobalIdentityKey == globalIdentityKey);

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> UpdatePatch(int id, Delta<Person> personPatch)
        {
            var person = await _repository.GetAsync<Person>(id);
            if (person == null)
            {
                var model = new PersonDto { ErrorMessage = "The person does not exist." };
                return model;
            }
            personPatch.CopyChangedValues(person);
            var result = await _repository.SaveAndReturnEntityAsync(person);
            return _mapper.Map<PersonDto>(result);
        }

        public async Task<BaseEntity> UpdatePersonContactDetails(PersonDto person)
        {
            BaseEntity result = null;
            var persistedPerson = _repository.CRMContext.Persons
                .AsNoTracking()
                .Include(p => p.PersonContactDetails)
                .FirstOrDefault(x => x.Id == person.Id || x.GlobalIdentityKey == person.GlobalIdentityKey);
            #region Add Contact By Detail
            var persistedContactDetailOptionIds = persistedPerson?.PersonContactDetails.Select(x => x.ContactByOptionId).ToList();
            var contactDetailAdded = person.PersonContactDetails.Where(x => x.Id == 0 && (persistedContactDetailOptionIds == null || !persistedContactDetailOptionIds.Contains(x.ContactByOptionId))).ToList();
            foreach (var contactDetailDto in contactDetailAdded)
            {
                var entity = Mapper.Map<PersonContactDetail>(contactDetailDto);
                entity.PersonId = person.Id;
                result = await _repository.SaveAndReturnEntityAsync(entity);
            }
            #endregion

            if (persistedPerson != null)
            {
                #region Update Contact By Detail
                foreach (var contactDetailDto in person.PersonContactDetails)
                {
                    var updatedEntity = person.PersonContactDetails.FirstOrDefault(x => x.ContactByOptionId == contactDetailDto.ContactByOptionId && (x.ContactValue != contactDetailDto.ContactValue || x.Comment != contactDetailDto.Comment));
                    if (updatedEntity == null) continue;
                    var entity = Mapper.Map<PersonContactDetail>(updatedEntity);
                    result = await _repository.SaveAndReturnEntityAsync(entity);
                }
                #endregion

                #region Delete Contact By Detail
                var contactByIds = person.PersonContactDetails.Select(x => x.ContactByOptionId).ToList();
                var deleteContactDetailIds = persistedPerson?.PersonContactDetails
                    .Where(x => !contactByIds.Contains(x.ContactByOptionId)).Select(x => x.Id).ToList();
                ICollection<PersonContactDetail> deleteContactDetails = _repository.CRMContext.PersonContactDetails
                    .Where(x => deleteContactDetailIds.Contains(x.Id)).ToList();
                await _repository.HardDeleteAsync(deleteContactDetails);
                #endregion
            }
            return result ?? new Person { Id = person.Id };// When only delete executed then the result will be null
        }

        public async Task<PersonDto> GetAsync(int id)
        {
            var person = await _repository.CRMContext.Persons
                .Include(x => x.Applications)
                .Include(x => x.Documents)
                .Include(x => x.Emails)
                .Include(x => x.MainContactPerson)
                .Include(x => x.PersonAddresses)
                .ThenInclude(x => x.Address)
                .Include(x => x.MergePersons)
                .Include(x => x.PersonContactDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonDto person)
        {
            var successMessage = person.Id == 0
                 ? "The person is created successfully in CRM System."
                 : "The person is updated successfully in CRM System.";
            var entity = Mapper.Map<Person>(person);
            entity.NationalInsuranceNumber = entity.NationalInsuranceNumber?.Replace(" ", "");
            entity.MainContactPersonId = person.Id == 0 ? null : person.MainContactPersonId == 0 ? entity.Id : entity.MainContactPersonId;
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            if (person.Id == 0)
            {
                entity.MainContactPersonId = result.Id;
                await _repository.SaveAndReturnEntityAsync(entity);
            }
            if (string.IsNullOrWhiteSpace(result.ErrorMessage))
                result.SuccessMessage = successMessage;
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = _repository.CRMContext.Persons
                .Include(p => p.Applications)
                .Include(p => p.PersonAddresses)
                .ThenInclude(a => a.Address)
                .Include(p => p.PersonContactDetails)
                .FirstOrDefault(x => x.Id == id);

            if (person == null)
            {
                return false;
            }
            await _repository.HardDeleteAsync(person.Applications);
            await _repository.HardDeleteAsync(person.PersonContactDetails);
            ICollection<Address> address = person.PersonAddresses.Select(x => x.Address).ToList();
            await _repository.HardDeleteAsync(address);
            await _repository.HardDeleteAsync(person.PersonAddresses);
            var result = await _repository.HardDeleteAsync(person);
            return result;
        }
    }
}
