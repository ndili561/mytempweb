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
    public class PersonDocumentController  : BaseController<BaseEntity>
    {
        private readonly IPersonDocumentService _personDocumentService;

        public PersonDocumentController(IPersonDocumentService personDocumentService)
        {
            _personDocumentService = personDocumentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personDocumentService.GetAsync());
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personDocumentService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonDocumentDto personDocumentDto)
        {
            if (personDocumentDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personDocumentService.SaveAndReturnEntityAsync(personDocumentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonDocumentDto personDocumentDto)
        {
            if (id == 0 || personDocumentDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personDocumentService.SaveAndReturnEntityAsync(personDocumentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personDocumentService.PersonDocumentExistsAsync(id),
                async () => await _personDocumentService.DeleteAsync(id));
        }
    }
}