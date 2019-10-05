using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonService
    {
        Task<PersonDto> GetAsync(int id);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonDto person);
        Task<bool> DeleteAsync(int id);
        Task<bool> PersonExistsAsync(int id);
        Task<PersonDto> GetAsyncByGlobalIdentityKey(Guid globalIdentityKey);
        Task<PersonDto> UpdatePatch(int id, Delta<Person> personPatch);
        Task<BaseEntity> UpdatePersonContactDetails(PersonDto person);
    }
}