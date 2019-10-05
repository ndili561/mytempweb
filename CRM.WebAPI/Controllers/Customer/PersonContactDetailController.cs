using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Customer
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonContactDetailController : BaseController<BaseEntity>
    {
        private readonly IPersonContactDetailService _personContactDetailService;

        public PersonContactDetailController(IPersonContactDetailService personContactDetailService)
        {
            _personContactDetailService = personContactDetailService;
        }

        [HttpGet("{options}", Name = "GetPersonContactDetails")]
        public PageResult<PersonContactDetailDto> GetPersonContactDetails(ODataQueryOptions<PersonContactDetail> options)
        {
            return _personContactDetailService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personContactDetailService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonContactDetailDto person)
        {
            if (person.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personContactDetailService.SaveAndReturnEntityAsync(person));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonContactDetailDto person)
        {
            if (id == 0 || person.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }
            await SaveAndReturnEntityAsync(async () => await _personContactDetailService.SaveAndReturnEntityAsync(person));
            return await GetSingleAsync(async () => await _personContactDetailService.GetAsync(id));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personContactDetailService.PersonContactDetailExistsAsync(id),
                async () => await _personContactDetailService.DeleteAsync(id));
        }

       
    }
}