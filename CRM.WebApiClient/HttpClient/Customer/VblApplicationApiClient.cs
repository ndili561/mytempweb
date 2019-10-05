using System.Threading.Tasks;
using CRM.Entity.Model.Customer;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Customer;
using Microsoft.Extensions.Options;

namespace CRM.WebApiClient.HttpClient.Customer
{
    /// <summary>
    /// </summary>
    public class VblApplicationApiClient : BaseClient, IVblApplicationApiClient
    {
        public VblApplicationApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<VblApplicationDto> GetVblApplication(int applicationId)
        {
            var url = AllocationApiUri + "/CrmApplications/GetApplication?applicationId=" + applicationId;
            var result = await GetResultFromApi<VblApplicationDto>(url);
            return result;
        }
    }
}
