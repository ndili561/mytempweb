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
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EthnicityController  : BaseController<BaseEntity>
    {
        private readonly IEthnicityService _ethnicityService;

        public EthnicityController(IEthnicityService ethnicityService)
        {
            _ethnicityService = ethnicityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ethnicityService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetEthnicities")]
        public PageResult<EthnicityDto> GetEthnicities(ODataQueryOptions<Ethnicity> options)
        {
            return _ethnicityService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _ethnicityService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EthnicityDto ethnicityDto)
        {
            if (ethnicityDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _ethnicityService.SaveAndReturnEntityAsync(ethnicityDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EthnicityDto ethnicityDto)
        {
            if (id == 0 || ethnicityDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _ethnicityService.SaveAndReturnEntityAsync(ethnicityDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _ethnicityService.EthnicityExistsAsync(id),
                async () => await _ethnicityService.DeleteAsync(id));
        }
    }
}