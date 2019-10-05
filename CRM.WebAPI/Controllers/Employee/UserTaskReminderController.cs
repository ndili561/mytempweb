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
    public class UserTaskReminderController  : BaseController<BaseEntity>
    {
        private readonly IUserTaskReminderService _userCalendarTaskReminderService;

        public UserTaskReminderController(IUserTaskReminderService userCalendarTaskReminderService)
        {
            _userCalendarTaskReminderService = userCalendarTaskReminderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userCalendarTaskReminderService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUserTaskReminders")]
        public PageResult<UserTaskReminderDto> GetUserCalendarTaskReminders(ODataQueryOptions<UserTaskReminder> options)
        {
            return _userCalendarTaskReminderService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userCalendarTaskReminderService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserTaskReminderDto userCalendarTaskReminder)
        {
            if (userCalendarTaskReminder.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userCalendarTaskReminderService.SaveAndReturnEntityAsync(userCalendarTaskReminder));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserTaskReminderDto userCalendarTaskReminder)
        {
            if (id == 0 || userCalendarTaskReminder.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userCalendarTaskReminderService.SaveAndReturnEntityAsync(userCalendarTaskReminder));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userCalendarTaskReminderService.UserCalendarTaskReminderExistsAsync(id),
                async () => await _userCalendarTaskReminderService.DeleteAsync(id));
        }
    }
}