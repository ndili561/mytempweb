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
    public class PersonFlagController  : BaseController<BaseEntity>
    {
        private readonly IPersonFlagService _personFlagService;

        public PersonFlagController(IPersonFlagService personFlagService)
        {
            _personFlagService = personFlagService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personFlagService.GetAsync());
        }
      
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personFlagService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonFlagDto personFlagDto)
        {
            if (personFlagDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personFlagService.SaveAndReturnEntityAsync(personFlagDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonFlagDto personFlagDto)
        {
            if (id == 0 || personFlagDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personFlagService.SaveAndReturnEntityAsync(personFlagDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personFlagService.PersonFlagExistsAsync(id),
                async () => await _personFlagService.DeleteAsync(id));
        }
    }
}