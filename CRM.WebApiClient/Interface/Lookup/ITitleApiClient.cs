using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ITitleApiClient
    {
        Task<TitleSearchModel> GetTitles(TitleSearchModel model);
        Task<TitleDto> GetTitle(int titleId);
        Task<TitleDto> PostTitle(TitleDto model);
        Task<TitleDto> PutTitle(int id, TitleDto model);
    }
}