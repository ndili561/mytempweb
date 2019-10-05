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
    public class UserDiaryController  : BaseController<BaseEntity>
    {
        private readonly IUserDiaryService _userDiaryService;

        public UserDiaryController(IUserDiaryService userDiaryService)
        {
            _userDiaryService = userDiaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userDiaryService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetUserDiarys")]
        public PageResult<UserDiaryDto> GetUserDiarys(ODataQueryOptions<UserDiary> options)
        {
            return _userDiaryService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userDiaryService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDiaryDto userDiary)
        {
            if (userDiary.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userDiaryService.SaveAndReturnEntityAsync(userDiary));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserDiaryDto userDiary)
        {
            if (id == 0 || userDiary.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userDiaryService.SaveAndReturnEntityAsync(userDiary));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _userDiaryService.UserDiaryExistsAsync(id),
                async () => await _userDiaryService.DeleteAsync(id));
        }
    }
}