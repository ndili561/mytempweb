using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AlertStatusController  : BaseController<BaseEntity>
    {
        private readonly IAlertStatusService _alertStatusService;

        public AlertStatusController(IAlertStatusService alertStatusService)
        {
            _alertStatusService = alertStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _alertStatusService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _alertStatusService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AlertStatusDto alertStatus)
        {
            if (alertStatus.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _alertStatusService.SaveAndReturnEntityAsync(alertStatus));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AlertStatusDto alertStatus)
        {
            if (id == 0 || alertStatus.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _alertStatusService.SaveAndReturnEntityAsync(alertStatus));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _alertStatusService.AlertStatusExistsAsync(id),
                async () => await _alertStatusService.DeleteAsync(id));
        }
    }
}