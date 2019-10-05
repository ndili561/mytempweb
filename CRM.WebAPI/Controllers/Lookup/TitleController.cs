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
    public class TitleController  : BaseController<BaseEntity>
    {
        private readonly ITitleService _titleService;

        public TitleController(ITitleService titleService)
        {
            _titleService = titleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _titleService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetTitles")]
        public PageResult<TitleDto> GetTitles(ODataQueryOptions<Title> options)
        {
            return _titleService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _titleService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TitleDto title)
        {
            if (title.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _titleService.SaveAndReturnEntityAsync(title));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TitleDto title)
        {
            if (id == 0 || title.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _titleService.SaveAndReturnEntityAsync(title));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _titleService.TitleExistsAsync(id),
                async () => await _titleService.DeleteAsync(id));
        }
    }
}