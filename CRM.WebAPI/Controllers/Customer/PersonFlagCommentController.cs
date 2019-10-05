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
    public class PersonFlagCommentController  : BaseController<BaseEntity>
    {
        private readonly IPersonFlagCommentService _personFlagCommentService;

        public PersonFlagCommentController(IPersonFlagCommentService personFlagCommentService)
        {
            _personFlagCommentService = personFlagCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personFlagCommentService.GetAsync());
        }
      
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personFlagCommentService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonFlagCommentDto personFlagCommentDto)
        {
            if (personFlagCommentDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personFlagCommentService.SaveAndReturnEntityAsync(personFlagCommentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonFlagCommentDto personFlagCommentDto)
        {
            if (id == 0 || personFlagCommentDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personFlagCommentService.SaveAndReturnEntityAsync(personFlagCommentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personFlagCommentService.PersonFlagCommentExistsAsync(id),
                async () => await _personFlagCommentService.DeleteAsync(id));
        }
    }
}