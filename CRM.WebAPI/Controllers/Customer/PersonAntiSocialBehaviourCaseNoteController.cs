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
    public class PersonAntiSocialBehaviourCaseNoteController  : BaseController<BaseEntity>
    {
        private readonly IPersonAntiSocialBehaviourCaseNoteService _personAntiSocialBehaviourService;

        public PersonAntiSocialBehaviourCaseNoteController(IPersonAntiSocialBehaviourCaseNoteService personAntiSocialBehaviourService)
        {
            _personAntiSocialBehaviourService = personAntiSocialBehaviourService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personAntiSocialBehaviourService.GetAsync());
        }
      
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personAntiSocialBehaviourService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonAntiSocialBehaviourCaseNoteDto personAntiSocialBehaviourDto)
        {
            if (personAntiSocialBehaviourDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAntiSocialBehaviourService.SaveAndReturnEntityAsync(personAntiSocialBehaviourDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonAntiSocialBehaviourCaseNoteDto personAntiSocialBehaviourDto)
        {
            if (id == 0 || personAntiSocialBehaviourDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAntiSocialBehaviourService.SaveAndReturnEntityAsync(personAntiSocialBehaviourDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personAntiSocialBehaviourService.PersonAntiSocialBehaviourCaseNoteExistsAsync(id),
                async () => await _personAntiSocialBehaviourService.DeleteAsync(id));
        }
    }
}