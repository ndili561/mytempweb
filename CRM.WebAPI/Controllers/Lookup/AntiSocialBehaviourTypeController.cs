using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AntiSocialBehaviourTypeController  : BaseController<BaseEntity>
    {
        private readonly IAntiSocialBehaviourTypeService _antiSocialBehaviourTypeService;

        public AntiSocialBehaviourTypeController(IAntiSocialBehaviourTypeService antiSocialBehaviourTypeService)
        {
            _antiSocialBehaviourTypeService = antiSocialBehaviourTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _antiSocialBehaviourTypeService.GetAsync());
        }
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _antiSocialBehaviourTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AntiSocialBehaviourTypeDto antiSocialBehaviourType)
        {
            if (antiSocialBehaviourType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _antiSocialBehaviourTypeService.SaveAndReturnEntityAsync(antiSocialBehaviourType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AntiSocialBehaviourTypeDto antiSocialBehaviourType)
        {
            if (id == 0 || antiSocialBehaviourType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _antiSocialBehaviourTypeService.SaveAndReturnEntityAsync(antiSocialBehaviourType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _antiSocialBehaviourTypeService.AntiSocialBehaviourTypeExistsAsync(id),
                async () => await _antiSocialBehaviourTypeService.DeleteAsync(id));
        }
    }
}