﻿using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    //bug: Please do not use ROUTE attribute as it add bug to the system
    [Produces("application/json")]
    public class PersonAlertController : ODataBaseController
    {
        public PersonAlertController(CRMContext crmContext):base(crmContext)
        {
        }

        [EnableQuery]
        public IQueryable<PersonAlert> Get() => CRMContext.PersonAlerts.AsQueryable();
       
    }
}