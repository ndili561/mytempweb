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
    public class PersonCaseStatusController  : BaseController<BaseEntity>
    {
        private readonly IPersonCaseStatusService _caseStatusService;

        public PersonCaseStatusController(IPersonCaseStatusService caseStatusService)
        {
            _caseStatusService = caseStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _caseStatusService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _caseStatusService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonCaseStatusDto caseStatus)
        {
            if (caseStatus.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _caseStatusService.SaveAndReturnEntityAsync(caseStatus));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonCaseStatusDto caseStatus)
        {
            if (id == 0 || caseStatus.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _caseStatusService.SaveAndReturnEntityAsync(caseStatus));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _caseStatusService.CaseStatusExistsAsync(id),
                async () => await _caseStatusService.DeleteAsync(id));
        }
    }
}