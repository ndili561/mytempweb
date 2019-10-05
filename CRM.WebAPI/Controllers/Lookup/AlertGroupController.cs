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
    public class AlertGroupController  : BaseController<BaseEntity>
    {
        private readonly IAlertGroupService _AlertGroupService;

        public AlertGroupController(IAlertGroupService AlertGroupService)
        {
            _AlertGroupService = AlertGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _AlertGroupService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _AlertGroupService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AlertGroupDto alertGroup)
        {
            if (alertGroup.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _AlertGroupService.SaveAndReturnEntityAsync(alertGroup));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AlertGroupDto alertGroup)
        {
            if (id == 0 || alertGroup.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _AlertGroupService.SaveAndReturnEntityAsync(alertGroup));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _AlertGroupService.AlertGroupExistsAsync(id),
                async () => await _AlertGroupService.DeleteAsync(id));
        }
    }
}