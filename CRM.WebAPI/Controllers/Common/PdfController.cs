using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Common
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PdfController  : BaseController<BaseEntity>
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost]
        public  IActionResult Post([FromBody]DocumentDto documentDto)
        {
            if (documentDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }
            return StatusCode(StatusCodes.Status200OK, _pdfService.CreateTemplatePdf().Result);
        }
    }
}