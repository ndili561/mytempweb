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
    public class EmailController  : BaseController<BaseEntity>
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _emailService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetEmailes")]
        public PageResult<EmailDto> GetEmailes(ODataQueryOptions<Email> options)
        {
            return _emailService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _emailService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmailDto emailDto)
        {
            if (emailDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _emailService.SaveAndReturnEntityAsync(emailDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EmailDto emailDto)
        {
            if (id == 0 || emailDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _emailService.SaveAndReturnEntityAsync(emailDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _emailService.EmailExistsAsync(id),
                async () => await _emailService.DeleteAsync(id));
        }
    }
}