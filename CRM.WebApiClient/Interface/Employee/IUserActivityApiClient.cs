using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserActivityApiClient
    {
        Task<UserActivitySearchModel> GetUserActivities(UserActivitySearchModel model);
        Task<UserActivityDto> GetUserActivity(int userActivityId);
        Task<UserActivityDto> PostUserActivity(UserActivityDto model);
        Task<UserActivityDto> PutUserActivity(int id, UserActivityDto model);
    }
}