using System.Threading.Tasks;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Common
{
    public interface IPropertyDetailViewService
    {
        PageResult<PropertyDetailViewDto> GetAllAsync(ODataQueryOptions<PropertyDetailView> options);
        Task<PropertyDetailViewDto> GetAsyncByPropertyCode(string propertyCode);
    }
}