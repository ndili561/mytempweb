using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonFlagCommentService
    {
        Task<PersonFlagCommentDto> GetAsync(int id);
        Task<int> SaveAsync(PersonFlagCommentDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonFlagCommentDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonFlagCommentDto>> GetAsync();
        Task<bool> PersonFlagCommentExistsAsync(int id);
    }
}