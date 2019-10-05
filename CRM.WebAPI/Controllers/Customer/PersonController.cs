using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Customer
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonController : BaseController<BaseEntity>
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonDto person)
        {
            if (person.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personService.SaveAndReturnEntityAsync(person));
        }

        [HttpPut]
        [Route("UpdatePersonContactDetails")]
        public async Task<IActionResult> UpdatePersonContactDetails(int id,[FromBody]PersonDto person)
        {
            if (person.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Cannot add contact by details for person having id = 0.");
            }
            return await SaveAndReturnEntityAsync(async () => await _personService.UpdatePersonContactDetails(person));
        }
     
        [HttpPut("{globalIdentityKey}")]
        [Route("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson(string globalIdentityKey, [FromBody]PersonDto person)
        {
            if (string.IsNullOrWhiteSpace(globalIdentityKey))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Global Identity Key must have a value.");
            }
            return await SaveAndReturnEntityAsync(async () => await _personService.SaveAndReturnEntityAsync(person));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonDto person)
        {
            if (id == 0 || person.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }
            await SaveAndReturnEntityAsync(async () => await _personService.SaveAndReturnEntityAsync(person));
            return await GetSingleAsync(async () => await _personService.GetAsync(id));
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, Delta<Person> personPatch)
        {
            var entity = await _personService.UpdatePatch(id, personPatch);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personService.PersonExistsAsync(id),
                async () => await _personService.DeleteAsync(id));
        }

       
    }
}