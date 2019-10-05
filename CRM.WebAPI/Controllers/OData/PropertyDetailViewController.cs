using System;
using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    [Produces("application/json")]
    public class PropertyDetailViewController : ODataBaseController
    {
        public PropertyDetailViewController(CRMContext crmContext):base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<PropertyDetailView> Get() => CRMContext.PropertyDetailView.AsQueryable();
        //[EnableQuery]
        //public IQueryable<PropertyDetailView> Get()
        //{
        //    try
        //    {
        //       var result = CRMContext.PropertyDetailView.AsQueryable();
        //        var test = result.FirstOrDefault();
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}
    }
}