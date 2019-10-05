using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NationalityTypeController  : BaseController<BaseEntity>
    {
        private readonly INationalityTypeService _nationalityTypeService;

        public NationalityTypeController(INationalityTypeService nationalityTypeService)
        {
            _nationalityTypeService = nationalityTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _nationalityTypeService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetNationalityTypes")]
        public PageResult<NationalityTypeDto> GetNationalityTypes(ODataQueryOptions<NationalityType> options)
        {
            return _nationalityTypeService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _nationalityTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NationalityTypeDto nationalityType)
        {
            if (nationalityType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _nationalityTypeService.SaveAndReturnEntityAsync(nationalityType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]NationalityTypeDto nationalityType)
        {
            if (id == 0 || nationalityType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _nationalityTypeService.SaveAndReturnEntityAsync(nationalityType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _nationalityTypeService.NationalityExistsAsync(id),
                async () => await _nationalityTypeService.DeleteAsync(id));
        }
    }
}