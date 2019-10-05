using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.BLL.Interface
{
    public interface IPersonBll
    {
        PageResult<PersonDto> GetAll(ODataQueryOptions<Person> options);
        IEnumerable<Person> GetAll();
        PersonDto Get(int id);
        PersonDto Add(PersonDto entity);
        Task<PersonDto> AddAsync(PersonDto entity);
        PersonDto Update(PersonDto entity);
        Task<PersonDto> UpdateAsync(PersonDto entity);
        void Delete(PersonDto operative);
        Task<bool> DeleteAsync(PersonDto entity);
    }
}
