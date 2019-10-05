using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Employee
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MessageController  : BaseController<BaseEntity>
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _messageService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetMessages")]
        public PageResult<MessageDto> GetMessages(ODataQueryOptions<Message> options)
        {
            return _messageService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _messageService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageDto message)
        {
            if (message.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _messageService.SaveAndReturnEntityAsync(message));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]MessageDto message)
        {
            if (id == 0 || message.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _messageService.SaveAndReturnEntityAsync(message));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _messageService.MessageExistsAsync(id),
                async () => await _messageService.DeleteAsync(id));
        }
    }
}