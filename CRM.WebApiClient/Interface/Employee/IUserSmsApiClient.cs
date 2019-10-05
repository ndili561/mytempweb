using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserSmsApiClient
    {
        Task<UserSmsSearchModel> GetUserSmses(UserSmsSearchModel model);
        Task<UserSmsDto> GetUserSms(int id);
        Task<UserSmsDto> PostUserSms(UserSmsDto model);
        Task<UserSmsDto> PutUserSms(int id, UserSmsDto model);
    }
}