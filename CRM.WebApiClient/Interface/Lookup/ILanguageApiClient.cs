using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface ILanguageApiClient
    {
        Task<LanguageSearchModel> GetLanguages(LanguageSearchModel model);
        Task<LanguageDto> GetLanguage(int languageId);
        Task<LanguageDto> PostLanguage(LanguageDto model);
        Task<LanguageDto> PutLanguage(int id,LanguageDto model);
    }
}