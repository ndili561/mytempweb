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
    public class AlertTypeController  : BaseController<BaseEntity>
    {
        private readonly IAlertTypeService _AlertTypeService;

        public AlertTypeController(IAlertTypeService AlertTypeService)
        {
            _AlertTypeService = AlertTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _AlertTypeService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _AlertTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AlertTypeDto alertType)
        {
            if (alertType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _AlertTypeService.SaveAndReturnEntityAsync(alertType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AlertTypeDto alertType)
        {
            if (id == 0 || alertType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _AlertTypeService.SaveAndReturnEntityAsync(alertType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _AlertTypeService.AlertTypeExistsAsync(id),
                async () => await _AlertTypeService.DeleteAsync(id));
        }
    }
}