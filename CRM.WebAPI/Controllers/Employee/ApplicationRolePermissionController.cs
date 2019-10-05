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
    public class PermissionController  : BaseController<BaseEntity>
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _permissionService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetPermissions")]
        public PageResult<PermissionDto> GetPermissions(ODataQueryOptions<Permission> options)
        {
            return _permissionService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _permissionService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PermissionDto permission)
        {
            if (permission.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _permissionService.SaveAndReturnEntityAsync(permission));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PermissionDto permission)
        {
            if (id == 0 || permission.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _permissionService.SaveAndReturnEntityAsync(permission));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _permissionService.PermissionExistsAsync(id),
                async () => await _permissionService.DeleteAsync(id));
        }
    }
}