using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Customer
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TenantController  : BaseController<BaseEntity>
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _tenantService.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _tenantService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TenantDto tenantDto)
        {
            if (tenantDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _tenantService.SaveAndReturnEntityAsync(tenantDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TenantDto tenantDto)
        {
            if (id == 0 || tenantDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _tenantService.SaveAndReturnEntityAsync(tenantDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _tenantService.TenantExistsAsync(id),
                async () => await _tenantService.DeleteAsync(id));
        }
    }
}