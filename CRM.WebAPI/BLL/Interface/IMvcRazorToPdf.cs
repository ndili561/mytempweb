using System.Threading.Tasks;

namespace CRM.WebAPI.BLL.Interface
{
    public interface IMvcRazorToPdf
    {
        Task<byte[]> GeneratePdfOutput(string viewName, dynamic model);
    }
}
