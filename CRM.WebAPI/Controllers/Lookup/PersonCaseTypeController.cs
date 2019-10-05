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
    public class PersonCaseTypeController  : BaseController<BaseEntity>
    {
        private readonly IPersonCaseTypeService _caseTypeService;

        public PersonCaseTypeController(IPersonCaseTypeService caseTypeService)
        {
            _caseTypeService = caseTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _caseTypeService.GetAsync());
        }
   
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _caseTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonCaseTypeDto caseType)
        {
            if (caseType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _caseTypeService.SaveAndReturnEntityAsync(caseType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PersonCaseTypeDto caseType)
        {
            if (id == 0 || caseType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _caseTypeService.SaveAndReturnEntityAsync(caseType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _caseTypeService.CaseTypeExistsAsync(id),
                async () => await _caseTypeService.DeleteAsync(id));
        }
    }
}