using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Employee
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WebApplicationController  : BaseController<BaseEntity>
    {
        private readonly IWebApplicationService _webApplicationService;

        public WebApplicationController(IWebApplicationService webApplicationService)
        {
            _webApplicationService = webApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _webApplicationService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetWebApplications")]
        public PageResult<WebApplicationDto> GetWebApplications(ODataQueryOptions<WebApplication> options)
        {
            return _webApplicationService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _webApplicationService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]WebApplicationDto webApplication)
        {
            if (webApplication.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _webApplicationService.SaveAndReturnEntityAsync(webApplication));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]WebApplicationDto webApplication)
        {
            if (id == 0 || webApplication.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _webApplicationService.SaveAndReturnEntityAsync(webApplication));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _webApplicationService.WebApplicationExistsAsync(id),
                async () => await _webApplicationService.DeleteAsync(id));
        }
    }
}