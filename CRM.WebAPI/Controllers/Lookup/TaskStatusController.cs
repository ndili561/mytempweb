using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = CRM.DAL.Database.Entities.Lookup.TaskStatus;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskStatusController  : BaseController<BaseEntity>
    {
        private readonly ITaskStatusService _taskStatusService;

        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _taskStatusService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetTaskStatuses")]
        public PageResult<TaskStatusDto> GetTaskStatuses(ODataQueryOptions<TaskStatus> options)
        {
            return _taskStatusService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _taskStatusService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskStatusDto taskStatus)
        {
            if (taskStatus.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskStatusService.SaveAndReturnEntityAsync(taskStatus));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskStatusDto taskStatus)
        {
            if (id == 0 || taskStatus.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskStatusService.SaveAndReturnEntityAsync(taskStatus));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _taskStatusService.TaskStatusExistsAsync(id),
                async () => await _taskStatusService.DeleteAsync(id));
        }
    }
}