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
    public class FlagTypeController  : BaseController<BaseEntity>
    {
        private readonly IFlagTypeService _FlagTypeService;

        public FlagTypeController(IFlagTypeService FlagTypeService)
        {
            _FlagTypeService = FlagTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _FlagTypeService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _FlagTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FlagTypeDto FlagType)
        {
            if (FlagType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _FlagTypeService.SaveAndReturnEntityAsync(FlagType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]FlagTypeDto FlagType)
        {
            if (id == 0 || FlagType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _FlagTypeService.SaveAndReturnEntityAsync(FlagType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _FlagTypeService.FlagTypeExistsAsync(id),
                async () => await _FlagTypeService.DeleteAsync(id));
        }
    }
}