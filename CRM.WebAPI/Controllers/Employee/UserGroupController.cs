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
    public class UserGroupController  : BaseController<BaseEntity>
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userGroupService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUserGroups")]
        public PageResult<UserGroupDto> GetUserGroups(ODataQueryOptions<UserGroup> options)
        {
            return _userGroupService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userGroupService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserGroupDto userGroup)
        {
            if (userGroup.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userGroupService.SaveAndReturnEntityAsync(userGroup));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserGroupDto userGroup)
        {
            if (id == 0 || userGroup.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userGroupService.SaveAndReturnEntityAsync(userGroup));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userGroupService.UserGroupExistsAsync(id),
                async () => await _userGroupService.DeleteAsync(id));
        }
    }
}