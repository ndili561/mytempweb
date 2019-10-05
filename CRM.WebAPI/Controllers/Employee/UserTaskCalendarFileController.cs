using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Employee
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserTaskCalendarFileController  : BaseController<BaseEntity>
    {
        private readonly IUserPersonTaskCalendarFileService _userPersonTaskCalendarFileService;

        public UserTaskCalendarFileController(IUserPersonTaskCalendarFileService userPersonTaskCalendarFileService)
        {
            _userPersonTaskCalendarFileService = userPersonTaskCalendarFileService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _userPersonTaskCalendarFileService.GetAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]UserDto entityDto)
        {
            if (id == 0 || entityDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _userPersonTaskCalendarFileService.SaveAndReturnEntityAsync(entityDto));
        }

      
    }
}