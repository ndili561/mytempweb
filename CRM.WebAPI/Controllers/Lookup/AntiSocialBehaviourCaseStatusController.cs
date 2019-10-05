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
    public class AntiSocialBehaviourCaseStatusController  : BaseController<BaseEntity>
    {
        private readonly IAntiSocialBehaviourCaseStatusService _antiSocialBehaviourCaseStatusService;

        public AntiSocialBehaviourCaseStatusController(IAntiSocialBehaviourCaseStatusService antiSocialBehaviourCaseStatusService)
        {
            _antiSocialBehaviourCaseStatusService = antiSocialBehaviourCaseStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _antiSocialBehaviourCaseStatusService.GetAsync());
        }
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _antiSocialBehaviourCaseStatusService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AntiSocialBehaviourCaseStatusDto antiSocialBehaviourCaseStatus)
        {
            if (antiSocialBehaviourCaseStatus.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _antiSocialBehaviourCaseStatusService.SaveAndReturnEntityAsync(antiSocialBehaviourCaseStatus));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AntiSocialBehaviourCaseStatusDto antiSocialBehaviourCaseStatus)
        {
            if (id == 0 || antiSocialBehaviourCaseStatus.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _antiSocialBehaviourCaseStatusService.SaveAndReturnEntityAsync(antiSocialBehaviourCaseStatus));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _antiSocialBehaviourCaseStatusService.AntiSocialBehaviourCaseStatusExistsAsync(id),
                async () => await _antiSocialBehaviourCaseStatusService.DeleteAsync(id));
        }
    }
}