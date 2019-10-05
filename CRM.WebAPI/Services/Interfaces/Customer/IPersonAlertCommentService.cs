using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Services.Interfaces.Customer
{
    public interface IPersonAlertCommentService
    {
        Task<PersonAlertCommentDto> GetAsync(int id);
        Task<int> SaveAsync(PersonAlertCommentDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(PersonAlertCommentDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonAlertCommentDto>> GetAsync();
        Task<bool> PersonAlertCommentExistsAsync(int id);
    }
}