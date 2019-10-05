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
    public class UserApplicationRoleController  : BaseController<BaseEntity>
    {
        private readonly IUserApplicationRoleService _userApplicationRoleService;

        public UserApplicationRoleController(IUserApplicationRoleService userApplicationRoleService)
        {
            _userApplicationRoleService = userApplicationRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userApplicationRoleService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUserApplicationRoles")]
        public PageResult<UserApplicationRoleDto> GetUserApplicationRoles(ODataQueryOptions<UserApplicationRole> options)
        {
            return _userApplicationRoleService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userApplicationRoleService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserApplicationRoleDto userApplicationRole)
        {
            if (userApplicationRole.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userApplicationRoleService.SaveAndReturnEntityAsync(userApplicationRole));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserApplicationRoleDto userApplicationRole)
        {
            if (id == 0 || userApplicationRole.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userApplicationRoleService.SaveAndReturnEntityAsync(userApplicationRole));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userApplicationRoleService.UserApplicationRoleExistsAsync(id),
                async () => await _userApplicationRoleService.DeleteAsync(id));
        }
    }
}