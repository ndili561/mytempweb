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
    public class PersonAddressController  : BaseController<BaseEntity>
    {
        private readonly IPersonAddressService _personAddressService;

        public PersonAddressController(IPersonAddressService personAddressService)
        {
            _personAddressService = personAddressService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personAddressService.GetAsync());
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personAddressService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonAddressDto personAddressDto)
        {
            if (personAddressDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAddressService.SaveAndReturnEntityAsync(personAddressDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonAddressDto personAddressDto)
        {
            if (id == 0 || personAddressDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAddressService.SaveAndReturnEntityAsync(personAddressDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personAddressService.PersonAddressExistsAsync(id),
                async () => await _personAddressService.DeleteAsync(id));
        }
    }
}