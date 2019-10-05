using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = CRM.DAL.Database.Entities.Employee.Task;

namespace CRM.WebAPI.Controllers.Employee
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TaskController  : BaseController<BaseEntity>
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _taskService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetTasks")]
        public PageResult<TaskDto> GetTasks(ODataQueryOptions<Task> options)
        {
            return _taskService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _taskService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskDto task)
        {
            if (task.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskService.SaveAndReturnEntityAsync(task));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskDto task)
        {
            if (id == 0 || task.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskService.SaveAndReturnEntityAsync(task));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _taskService.TaskExistsAsync(id),
                async () => await _taskService.DeleteAsync(id));
        }
    }
}