using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IUserDocumentApiClient
    {
        Task<UserDocumentSearchModel> GetUserDocuments(UserDocumentSearchModel model);
        Task<UserDocumentDto> GetUserDocument(int id);
        Task<UserDocumentDto> PostUserDocument(UserDocumentDto model);
        Task<UserDocumentDto> PutUserDocument(int id, UserDocumentDto model);
    }
}