﻿using System.Linq;
using CRM.DAL.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    [Produces("application/json")]
    public class LookupController : ODataBaseController
    {

        public LookupController(CRMContext crmContext):base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<DAL.Database.Entities.Lookup.Lookup> Get() => CRMContext.Lookups.AsQueryable();
    }
}