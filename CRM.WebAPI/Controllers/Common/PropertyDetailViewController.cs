using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Common
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PropertyDetailViewController : BaseController<BaseEntity>
    {
        private readonly IPropertyDetailViewService _propertyDetailView;

        public PropertyDetailViewController(IPropertyDetailViewService propertyDetailView)
        {
            _propertyDetailView = propertyDetailView;
        }
        //[HttpGet("{options}", Name = "GetPropertyDetailViews")]
        //public PageResult<PropertyDetailViewDto> GetPropertyDetailViews(ODataQueryOptions<PropertyDetailView> options)
        //{
        //    return _propertyDetailView.GetAllAsync(options);
        //}
        [HttpGet]
        public async Task<IActionResult> Get(string propertyCode)
        {
            return await GetSingleAsync(async () => await _propertyDetailView.GetAsyncByPropertyCode(propertyCode));
        }
    }
}