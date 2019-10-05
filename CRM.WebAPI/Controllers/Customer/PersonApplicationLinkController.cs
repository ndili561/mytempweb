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
    public class PersonApplicationLinkController  : BaseController<BaseEntity>
    {
        private readonly IPersonApplicationLinkService _personApplicationLinkService;

        public PersonApplicationLinkController(IPersonApplicationLinkService personApplicationLinkService)
        {
            _personApplicationLinkService = personApplicationLinkService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personApplicationLinkService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetPersonApplicationLinkes")]
        public PageResult<PersonApplicationLinkDto> GetPersonApplicationLinkes(ODataQueryOptions<PersonApplicationLink> options)
        {
            return _personApplicationLinkService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _personApplicationLinkService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonApplicationLinkDto personApplicationLinkDto)
        {
            if (personApplicationLinkDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personApplicationLinkService.SaveAndReturnEntityAsync(personApplicationLinkDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonApplicationLinkDto personApplicationLinkDto)
        {
            if (id == 0 || personApplicationLinkDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _personApplicationLinkService.SaveAndReturnEntityAsync(personApplicationLinkDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _personApplicationLinkService.PersonApplicationLinkExistsAsync(id),
                async () => await _personApplicationLinkService.DeleteAsync(id));
        }
    }
}