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
    public class PersonCaseController  : BaseController<BaseEntity>
    {
        private readonly IPersonCaseService _personCaseService;

        public PersonCaseController(IPersonCaseService personCaseService)
        {
            _personCaseService = personCaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personCaseService.GetAsync());
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personCaseService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonCaseDto personCaseDto)
        {
            if (personCaseDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personCaseService.SaveAndReturnEntityAsync(personCaseDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonCaseDto personCaseDto)
        {
            if (id == 0 || personCaseDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personCaseService.SaveAndReturnEntityAsync(personCaseDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personCaseService.PersonCaseExistsAsync(id),
                async () => await _personCaseService.DeleteAsync(id));
        }
    }
}