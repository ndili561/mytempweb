using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonFlagCommentApiClient
    {
        Task<PersonFlagCommentSearchModel> GetPersonFlagComments(PersonFlagCommentSearchModel model);
        Task<PersonFlagCommentDto> GetPersonFlagComment(int personFlagCommentId);
        Task<PersonFlagCommentDto> PostPersonFlagComment(PersonFlagCommentDto model);
        Task<PersonFlagCommentDto> PutPersonFlagComment(int id, PersonFlagCommentDto model);
    }
}