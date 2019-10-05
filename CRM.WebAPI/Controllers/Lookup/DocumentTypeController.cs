using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DocumentTypeController  : BaseController<BaseEntity>
    {
        private readonly IDocumentTypeService _documentTypeService;


        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _documentTypeService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetDocumentTypes")]
        public PageResult<DocumentTypeDto> GetDocumentTypes(ODataQueryOptions<DocumentType> options)
        {
            return _documentTypeService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _documentTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DocumentTypeDto documentType)
        {
            if (documentType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _documentTypeService.SaveAndReturnEntityAsync(documentType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DocumentTypeDto documentType)
        {
            if (id == 0 || documentType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _documentTypeService.SaveAndReturnEntityAsync(documentType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _documentTypeService.DocumentTypeExistsAsync(id),
                async () => await _documentTypeService.DeleteAsync(id));
        }
    }
}