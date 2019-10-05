using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PriorityController  : BaseController<BaseEntity>
    {
        private readonly IPriorityService _PriorityService;

        public PriorityController(IPriorityService PriorityService)
        {
            _PriorityService = PriorityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _PriorityService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _PriorityService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PriorityDto Priority)
        {
            if (Priority.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _PriorityService.SaveAndReturnEntityAsync(Priority));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PriorityDto Priority)
        {
            if (id == 0 || Priority.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _PriorityService.SaveAndReturnEntityAsync(Priority));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _PriorityService.PriorityExistsAsync(id),
                async () => await _PriorityService.DeleteAsync(id));
        }
    }
}