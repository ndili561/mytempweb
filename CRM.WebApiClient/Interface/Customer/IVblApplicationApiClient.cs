using System.Threading.Tasks;
using CRM.Entity.Model.Customer;

namespace CRM.WebApiClient.Interface.Customer
{
    /// <summary>
    /// </summary>
    public interface IVblApplicationApiClient
    {
        Task<VblApplicationDto> GetVblApplication(int applicationId);
        
    }
}