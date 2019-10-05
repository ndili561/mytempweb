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
    public class UserActivityController  : BaseController<BaseEntity>
    {
        private readonly IUserActivityService _userActivityService;

        public UserActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userActivityService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUserActivitys")]
        public PageResult<UserActivityDto> GetUserActivitys(ODataQueryOptions<UserActivity> options)
        {
            return _userActivityService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userActivityService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserActivityDto userActivity)
        {
            if (userActivity.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userActivityService.SaveAndReturnEntityAsync(userActivity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserActivityDto userActivity)
        {
            if (id == 0 || userActivity.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userActivityService.SaveAndReturnEntityAsync(userActivity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userActivityService.UserActivityExistsAsync(id),
                async () => await _userActivityService.DeleteAsync(id));
        }
    }
}