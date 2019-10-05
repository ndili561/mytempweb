using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Search.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IPersonAlertApiClient
    {
        Task<PersonAlertSearchModel> GetPersonAlerts(PersonAlertSearchModel model);
        Task<PersonAlertDto> GetPersonAlert(int personAlertId);
        Task<PersonAlertDto> PostPersonAlert(PersonAlertDto model);
        Task<PersonAlertDto> PutPersonAlert(int id, PersonAlertDto model);
    }
}