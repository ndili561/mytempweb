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
    public class TaskTypeController  : BaseController<BaseEntity>
    {
        private readonly ITaskTypeService _taskTypeService;

        public TaskTypeController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _taskTypeService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetTaskTypes")]
        public PageResult<TaskTypeDto> GetTaskTypes(ODataQueryOptions<TaskType> options)
        {
            return _taskTypeService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _taskTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskTypeDto taskType)
        {
            if (taskType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskTypeService.SaveAndReturnEntityAsync(taskType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskTypeDto taskType)
        {
            if (id == 0 || taskType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _taskTypeService.SaveAndReturnEntityAsync(taskType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _taskTypeService.TaskTypeExistsAsync(id),
                async () => await _taskTypeService.DeleteAsync(id));
        }
    }
}