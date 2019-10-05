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
    public class PersonAlertCommentController  : BaseController<BaseEntity>
    {
        private readonly IPersonAlertCommentService _personAlertCommentService;

        public PersonAlertCommentController(IPersonAlertCommentService personAlertCommentService)
        {
            _personAlertCommentService = personAlertCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personAlertCommentService.GetAsync());
        }
      
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personAlertCommentService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonAlertCommentDto personAlertCommentDto)
        {
            if (personAlertCommentDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAlertCommentService.SaveAndReturnEntityAsync(personAlertCommentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonAlertCommentDto personAlertCommentDto)
        {
            if (id == 0 || personAlertCommentDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personAlertCommentService.SaveAndReturnEntityAsync(personAlertCommentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personAlertCommentService.PersonAlertCommentExistsAsync(id),
                async () => await _personAlertCommentService.DeleteAsync(id));
        }
    }
}