using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonDocumentApiClient
    {
        Task<PersonDocumentSearchModel> GetPersonDocuments(PersonDocumentSearchModel model);
        Task<PersonDocumentDto> GetPersonDocument(int id);
        Task<PersonDocumentDto> PostPersonDocument(PersonDocumentDto model);
        Task<PersonDocumentDto> PutPersonDocument(int id, PersonDocumentDto model);
    }
}