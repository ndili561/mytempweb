using CRM.DAL.Database.Entities.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.BLL.Interface
{
    public interface IAuditBll
    {
        PageResult<Audit> GetAll(ODataQueryOptions<Audit> options);
        Audit Get(int id);
    }
}
