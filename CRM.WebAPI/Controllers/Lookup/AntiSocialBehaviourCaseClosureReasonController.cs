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
    public class AntiSocialBehaviourCaseClosureReasonController  : BaseController<BaseEntity>
    {
        private readonly IAntiSocialBehaviourCaseClosureReasonService _antiSocialBehaviourCaseClosureReasonService;

        public AntiSocialBehaviourCaseClosureReasonController(IAntiSocialBehaviourCaseClosureReasonService antiSocialBehaviourCaseClosureReasonService)
        {
            _antiSocialBehaviourCaseClosureReasonService = antiSocialBehaviourCaseClosureReasonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _antiSocialBehaviourCaseClosureReasonService.GetAsync());
        }
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _antiSocialBehaviourCaseClosureReasonService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AntiSocialBehaviourCaseClosureReasonDto antiSocialBehaviourCaseClosureReason)
        {
            if (antiSocialBehaviourCaseClosureReason.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _antiSocialBehaviourCaseClosureReasonService.SaveAndReturnEntityAsync(antiSocialBehaviourCaseClosureReason));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AntiSocialBehaviourCaseClosureReasonDto antiSocialBehaviourCaseClosureReason)
        {
            if (id == 0 || antiSocialBehaviourCaseClosureReason.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _antiSocialBehaviourCaseClosureReasonService.SaveAndReturnEntityAsync(antiSocialBehaviourCaseClosureReason));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _antiSocialBehaviourCaseClosureReasonService.AntiSocialBehaviourCaseClosureReasonExistsAsync(id),
                async () => await _antiSocialBehaviourCaseClosureReasonService.DeleteAsync(id));
        }
    }
}