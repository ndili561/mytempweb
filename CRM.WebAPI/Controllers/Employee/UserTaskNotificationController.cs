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
    public class UserTaskNotificationController  : BaseController<BaseEntity>
    {
        private readonly IUserTaskNotificationService _userCalendarTaskNotificationService;

        public UserTaskNotificationController(IUserTaskNotificationService userCalendarTaskNotificationService)
        {
            _userCalendarTaskNotificationService = userCalendarTaskNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userCalendarTaskNotificationService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUserTaskNotifications")]
        public PageResult<UserTaskNotificationDto> GetUserCalendarTaskNotifications(ODataQueryOptions<UserTaskNotification> options)
        {
            return _userCalendarTaskNotificationService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userCalendarTaskNotificationService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserTaskNotificationDto userCalendarTaskNotification)
        {
            if (userCalendarTaskNotification.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userCalendarTaskNotificationService.SaveAndReturnEntityAsync(userCalendarTaskNotification));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserTaskNotificationDto userCalendarTaskNotification)
        {
            if (id == 0 || userCalendarTaskNotification.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userCalendarTaskNotificationService.SaveAndReturnEntityAsync(userCalendarTaskNotification));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userCalendarTaskNotificationService.UserCalendarTaskNotificationExistsAsync(id),
                async () => await _userCalendarTaskNotificationService.DeleteAsync(id));
        }
    }
}