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
    public class RoleController  : BaseController<BaseEntity>
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetAsync());
        }
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _roleService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RoleDto role)
        {
            if (role.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _roleService.SaveAndReturnEntityAsync(role));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]RoleDto role)
        {
            if (id == 0 || role.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _roleService.SaveAndReturnEntityAsync(role));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _roleService.RoleExistsAsync(id),
                async () => await _roleService.DeleteAsync(id));
        }
    }
}