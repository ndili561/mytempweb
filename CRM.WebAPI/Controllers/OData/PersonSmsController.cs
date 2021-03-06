﻿using System.Linq;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.OData
{

    [Produces("application/json")]
    public class PersonSmsController : ODataController
    {
        private readonly CRMContext _crmContext;

        public PersonSmsController(CRMContext crmContext)
        {
            _crmContext = crmContext;
        }

        [EnableQuery]
        public IQueryable<PersonSms> Get() => _crmContext.PersonSmses.AsQueryable();
    }
}