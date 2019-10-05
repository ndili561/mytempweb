using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskNotificationTypeController  : BaseController<BaseEntity>
    {
        private readonly ITaskNotificationTypeService _taskNotificationTypeService;

        public TaskNotificationTypeController(ITaskNotificationTypeService taskNotificationTypeService)
        {
            _taskNotificationTypeService = taskNotificationTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _taskNotificationTypeService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetTaskNotificationTypes")]
        public PageResult<TaskNotificationTypeDto> GetTaskNotificationTypes(ODataQueryOptions<TaskNotificationType> options)
        {
            return _taskNotificationTypeService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _taskNotificationTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskNotificationTypeDto taskNotificationType)
        {
            if (taskNotificationType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskNotificationTypeService.SaveAndReturnEntityAsync(taskNotificationType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskNotificationTypeDto taskNotificationType)
        {
            if (id == 0 || taskNotificationType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskNotificationTypeService.SaveAndReturnEntityAsync(taskNotificationType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _taskNotificationTypeService.TaskNotificationTypeExistsAsync(id),
                async () => await _taskNotificationTypeService.DeleteAsync(id));
        }
    }
}