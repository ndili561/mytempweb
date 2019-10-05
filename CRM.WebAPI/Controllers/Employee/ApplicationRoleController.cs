using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Employee
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ApplicationRoleController  : BaseController<BaseEntity>
    {
        private readonly IApplicationRoleService _applicationRoleService;

        public ApplicationRoleController(IApplicationRoleService applicationRoleService)
        {
            _applicationRoleService = applicationRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _applicationRoleService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetApplicationRoles")]
        public PageResult<ApplicationRoleDto> GetApplicationRoles(ODataQueryOptions<ApplicationRole> options)
        {
            return _applicationRoleService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _applicationRoleService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ApplicationRoleDto applicationRole)
        {
            if (applicationRole.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _applicationRoleService.SaveAndReturnEntityAsync(applicationRole));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ApplicationRoleDto applicationRole)
        {
            if (id == 0 || applicationRole.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _applicationRoleService.SaveAndReturnEntityAsync(applicationRole));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _applicationRoleService.ApplicationRoleExistsAsync(id),
                async () => await _applicationRoleService.DeleteAsync(id));
        }
    }
}