using System.Collections.Generic;
using AutoMapper;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using CRM.WebAPI.BLL.Interface;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Employee
{
    [Route("api/[controller]")]
    public class AuditController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuditBll _auditBll;

        public AuditController(IMapper mapper, IAuditBll auditBll)
        {
            _mapper = mapper;
            _auditBll = auditBll;

        }
        [HttpGet("{options}", Name = "GetAuditLogs")]
        public PageResult<AuditDto> GetAuditLogs(ODataQueryOptions<Audit> options)
        {
            var auditRecords = _auditBll.GetAll(options);
            var auditDtos = _mapper.Map<List<AuditDto>>(auditRecords);
            foreach (var auditDto in auditDtos)
            {
                auditDto.User.Audits.Clear();
            }
            return new PageResult<AuditDto>(auditDtos, null, auditRecords.Count);
        }
    }
}
