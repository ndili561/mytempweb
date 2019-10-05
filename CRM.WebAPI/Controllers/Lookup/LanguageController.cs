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
    public class LanguageController  : BaseController<BaseEntity>
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _languageService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetLanguages")]
        public PageResult<LanguageDto> GetLanguages(ODataQueryOptions<Language> options)
        {
            return _languageService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _languageService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LanguageDto language)
        {
            if (language.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _languageService.SaveAndReturnEntityAsync(language));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]LanguageDto language)
        {
            if (id == 0 || language.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _languageService.SaveAndReturnEntityAsync(language));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _languageService.LanguageExistsAsync(id),
                async () => await _languageService.DeleteAsync(id));
        }
    }
}