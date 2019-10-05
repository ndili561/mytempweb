using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
 
    [Route("api/[controller]")]
    public class GenderController  : BaseController<BaseEntity>
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _genderService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetGenders")]
        public PageResult<GenderDto> GetGenders(ODataQueryOptions<Gender> options)
        {
            return _genderService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _genderService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GenderDto gender)
        {
            if (gender.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _genderService.SaveAndReturnEntityAsync(gender));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]GenderDto gender)
        {
            if (id == 0 || gender.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _genderService.SaveAndReturnEntityAsync(gender));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _genderService.GenderExistsAsync(id),
                async () => await _genderService.DeleteAsync(id));
        }
    }
}