using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Common
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DocumentController  : BaseController<BaseEntity>
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _documentService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetDocumentes")]
        public PageResult<DocumentDto> GetDocumentes(ODataQueryOptions<Document> options)
        {
            return _documentService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _documentService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DocumentDto documentDto)
        {
            if (documentDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _documentService.SaveAndReturnEntityAsync(documentDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DocumentDto documentDto)
        {
            if (id == 0 || documentDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _documentService.SaveAndReturnEntityAsync(documentDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _documentService.DocumentExistsAsync(id),
                async () => await _documentService.DeleteAsync(id));
        }
    }
}