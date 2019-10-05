using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserDiaryApiClient
    {
        Task<UserDiarySearchModel> GetUserDiarys(UserDiarySearchModel model);
        Task<UserDiaryDto> GetUserDiary(int UserDiaryId);
        Task<UserDiaryDto> PostUserDiary(UserDiaryDto model);
        Task<UserDiaryDto> PutUserDiary(int id, UserDiaryDto model);
    }
}