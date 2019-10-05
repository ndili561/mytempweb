using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonAlertCommentApiClient
    {
        Task<PersonAlertCommentSearchModel> GetPersonAlertComments(PersonAlertCommentSearchModel model);
        Task<PersonAlertCommentDto> GetPersonAlertComment(int personAlertCommentId);
        Task<PersonAlertCommentDto> PostPersonAlertComment(PersonAlertCommentDto model);
        Task<PersonAlertCommentDto> PutPersonAlertComment(int id, PersonAlertCommentDto model);
    }
}