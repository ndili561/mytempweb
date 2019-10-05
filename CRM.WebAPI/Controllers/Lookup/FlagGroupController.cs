using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FlagGroupController  : BaseController<BaseEntity>
    {
        private readonly IFlagGroupService _FlagGroupService;

        public FlagGroupController(IFlagGroupService FlagGroupService)
        {
            _FlagGroupService = FlagGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _FlagGroupService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _FlagGroupService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FlagGroupDto FlagGroup)
        {
            if (FlagGroup.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _FlagGroupService.SaveAndReturnEntityAsync(FlagGroup));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]FlagGroupDto FlagGroup)
        {
            if (id == 0 || FlagGroup.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _FlagGroupService.SaveAndReturnEntityAsync(FlagGroup));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _FlagGroupService.FlagGroupExistsAsync(id),
                async () => await _FlagGroupService.DeleteAsync(id));
        }
    }
}