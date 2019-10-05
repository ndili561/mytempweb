using System.Threading.Tasks;
using CRM.DAL.Database.Entities.Common;
using CRM.WebAPI.BLL.Interface;
using CRM.WebAPI.Services.Interfaces.Common;

namespace CRM.WebAPI.Services.Common
{
    public class PdfService : IPdfService
    {
        private readonly IMvcRazorToPdf _mvcRazorToPdf;
        public PdfService(IMvcRazorToPdf mvcRazorToPdf)
        {
            _mvcRazorToPdf = mvcRazorToPdf;
        }

        public async Task<Document> CreateTemplatePdf()
        {
            var model = new Document ();
            await  _mvcRazorToPdf.GeneratePdfOutput("PurchaseOrderPdf", model);
            return model;
        }
    }
}
