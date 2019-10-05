using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Employee;
using CRM.WebAPI.BLL.Interface;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.BLL
{
    public class AuditBll: BaseBll, IAuditBll
    { 
        public AuditBll(CRMContext context):base(context) 
        {
        }

        public PageResult<Audit> GetAll(ODataQueryOptions<Audit> options)
        {
            var items= Context.Audits.Include(x=>x.User).Take(int.MaxValue).AsQueryable();
            var count = items.Count();
            if (options.OrderBy != null)
                items = options.OrderBy.ApplyTo(items);  //perform sort 
            if (options.Filter != null)
                items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Audit>();  //perform filter 
           
           count = items.ToList().Count;


            if (options.Skip != null)
                items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
            if (options.Top != null)
                items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
            return new PageResult<Audit>(items, null, count);
        }

        public Audit Get(int id)
        {
            return Context.Audits.FirstOrDefault(t => t.Id == id);
        }

        
    }
}
