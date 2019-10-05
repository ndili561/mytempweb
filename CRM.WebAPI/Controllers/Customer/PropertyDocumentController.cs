using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Customer
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PropertyDocumentController  : BaseController<BaseEntity>
    {
        private readonly IPropertyDocumentService _propertyDocumentService;

        public PropertyDocumentController(IPropertyDocumentService propertyDocumentService)
        {
            _propertyDocumentService = propertyDocumentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _propertyDocumentService.GetAsync());
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _propertyDocumentService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PropertyDocumentDto propertyDocumentDto)
        {
            if (propertyDocumentDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _propertyDocumentService.SaveAndReturnEntityAsync(propertyDocumentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PropertyDocumentDto propertyDocumentDto)
        {
            if (id == 0 || propertyDocumentDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _propertyDocumentService.SaveAndReturnEntityAsync(propertyDocumentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _propertyDocumentService.PropertyDocumentExistsAsync(id),
                async () => await _propertyDocumentService.DeleteAsync(id));
        }
    }
}