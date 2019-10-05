using System.Threading.Tasks;

namespace CRM.WebAPI.BLL.Interface
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
