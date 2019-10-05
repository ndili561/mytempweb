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
    public class TaskReminderTypeController  : BaseController<BaseEntity>
    {
        private readonly ITaskReminderTypeService _taskReminderTypeService;

        public TaskReminderTypeController(ITaskReminderTypeService taskReminderTypeService)
        {
            _taskReminderTypeService = taskReminderTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _taskReminderTypeService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetTaskReminderTypes")]
        public PageResult<TaskReminderTypeDto> GetTaskReminderTypes(ODataQueryOptions<TaskReminderType> options)
        {
            return _taskReminderTypeService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _taskReminderTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskReminderTypeDto taskReminderType)
        {
            if (taskReminderType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskReminderTypeService.SaveAndReturnEntityAsync(taskReminderType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskReminderTypeDto taskReminderType)
        {
            if (id == 0 || taskReminderType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskReminderTypeService.SaveAndReturnEntityAsync(taskReminderType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _taskReminderTypeService.TaskReminderTypeExistsAsync(id),
                async () => await _taskReminderTypeService.DeleteAsync(id));
        }
    }
}