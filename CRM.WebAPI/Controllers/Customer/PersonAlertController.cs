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
    public class PersonAlertController  : BaseController<BaseEntity>
    {
        private readonly IPersonAlertService _personAlertService;

        public PersonAlertController(IPersonAlertService personAlertService)
        {
            _personAlertService = personAlertService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personAlertService.GetAsync());
        }
      
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personAlertService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonAlertDto personAlertDto)
        {
            if (personAlertDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAlertService.SaveAndReturnEntityAsync(personAlertDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonAlertDto personAlertDto)
        {
            if (id == 0 || personAlertDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAlertService.SaveAndReturnEntityAsync(personAlertDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personAlertService.PersonAlertExistsAsync(id),
                async () => await _personAlertService.DeleteAsync(id));
        }
    }
}