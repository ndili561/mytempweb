using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Common
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SmsController  : BaseController<BaseEntity>
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _smsService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetSmses")]
        public PageResult<SmsDto> GetSmses(ODataQueryOptions<Sms> options)
        {
            return _smsService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _smsService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SmsDto smsDto)
        {
            if (smsDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _smsService.SaveAndReturnEntityAsync(smsDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]SmsDto smsDto)
        {
            if (id == 0 || smsDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _smsService.SaveAndReturnEntityAsync(smsDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _smsService.SmsExistsAsync(id),
                async () => await _smsService.DeleteAsync(id));
        }
    }
}