using System.Threading.Tasks;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IAuditApiClient
    {
        Task<AuditSearchModel> GetAudits( AuditSearchModel model);
    }
}