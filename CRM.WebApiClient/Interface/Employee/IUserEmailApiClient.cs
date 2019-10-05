using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserEmailApiClient
    {
        Task<UserEmailSearchModel> GetUserEmails(UserEmailSearchModel model);
        Task<UserEmailDto> GetUserEmail(int id);
        Task<UserEmailDto> PostUserEmail(UserEmailDto model);
        Task<UserEmailDto> PutUserEmail(int id, UserEmailDto model);
    }
}