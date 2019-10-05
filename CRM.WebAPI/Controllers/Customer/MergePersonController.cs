using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Customer
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MergePersonController  : BaseController<BaseEntity>
    {
        private readonly IMergePersonService _mergePersonService;

        public MergePersonController(IMergePersonService mergePersonService)
        {
            _mergePersonService = mergePersonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mergePersonService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetMergePersons")]
        public PageResult<MergePersonDto> GetMergePersons(ODataQueryOptions<MergePerson> options)
        {
            return _mergePersonService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _mergePersonService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MergePersonDto mergePersonDto)
        {
            if (mergePersonDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _mergePersonService.SaveAndReturnEntityAsync(mergePersonDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]MergePersonDto mergePersonDto)
        {
            if (id == 0 || mergePersonDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _mergePersonService.SaveAndReturnEntityAsync(mergePersonDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _mergePersonService.MergePersonExistsAsync(id),
                async () => await _mergePersonService.DeleteAsync(id));
        }
    }
}