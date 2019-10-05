using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Customer;
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
    public class UserController  : BaseController<BaseEntity>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUsers")]
        public PageResult<UserDto> GetUsers(ODataQueryOptions<User> options)
        {
            return _userService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await GetSingleAsync(async () => await _userService.GetAsync(id));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto user)
        {
            if (user.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userService.SaveAndReturnEntityAsync(user));
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, Delta<User> userPatch)
        {
            if (id == 0 )
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }
            var entity = await _userService.UpdatePatch(id, userPatch);
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserDto user)
        {
            if (id == 0 || user.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userService.SaveAndReturnEntityAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userService.UserExistsAsync(id),
                async () => await _userService.DeleteAsync(id));
        }
    }
}