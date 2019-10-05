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
    public class ChatMessageController  : BaseController<BaseEntity>
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _chatMessageService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetChatMessages")]
        public PageResult<ChatMessageDto> GetChatMessages(ODataQueryOptions<ChatMessage> options)
        {
            return _chatMessageService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _chatMessageService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ChatMessageDto chatMessage)
        {
            if (chatMessage.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _chatMessageService.SaveAndReturnEntityAsync(chatMessage));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ChatMessageDto chatMessage)
        {
            if (id == 0 || chatMessage.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _chatMessageService.SaveAndReturnEntityAsync(chatMessage));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _chatMessageService.ChatMessageExistsAsync(id),
                async () => await _chatMessageService.DeleteAsync(id));
        }
    }
}