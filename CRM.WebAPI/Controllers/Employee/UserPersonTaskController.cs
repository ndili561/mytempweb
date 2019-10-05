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
    public class UserPersonTaskController : BaseController<BaseEntity>
    {
        private readonly IUserPersonTaskService _userCalendarTaskService;

        public UserPersonTaskController(IUserPersonTaskService userCalendarTaskService)
        {
            _userCalendarTaskService = userCalendarTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userCalendarTaskService.GetAsync());
        }
        [HttpGet("{options}", Name = "UserPersonTask")]
        public PageResult<UserPersonTaskDto> GetUserCalendarTasks(ODataQueryOptions<UserPersonTask> options)
        {
            return _userCalendarTaskService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userCalendarTaskService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserPersonTaskDto userCalendarTask)
        {
            if (userCalendarTask.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userCalendarTaskService.SaveAndReturnEntityAsync(userCalendarTask));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserPersonTaskDto userCalendarTask)
        {
            if (id == 0 || userCalendarTask.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userCalendarTaskService.SaveAndReturnEntityAsync(userCalendarTask));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userCalendarTaskService.UserCalendarTaskExistsAsync(id),
                async () => await _userCalendarTaskService.DeleteAsync(id));
        }
    }
}