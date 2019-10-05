using System.Net.Http;
using System.Threading.Tasks;
using CRM.Entity.Model.Asset;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.Entity.Search.Customer;
using CRM.WebApiClient.Interface.Common;
using CRM.WebApiClient.Interface.Customer;
using CRM.WebApiClient.Interface.Facade;

namespace CRM.WebApiClient.HttpClient.Facade
{
    public class PropertyFacadeApiClient: IPropertyFacadeApiClient
    {
        private readonly IPropertyApiClient _propertyApiClient;
        private readonly IPropertyVoidApiClient _propertyVoidApiClient;
        private readonly IPropertyDocumentApiClient _propertyDocumentApiClient;
        private readonly IPropertyDetailViewApiClient _propertyDetailViewApiClient;
        private readonly IRepairApiClient _repairApiClient;
        private readonly ITenantApiClient _tenantApiClient;
        private readonly ITenantHistoryViewApiClient _tenantHistoryViewApiClient;
        public PropertyFacadeApiClient(
            IPropertyApiClient propertyApiClient,
            IPropertyVoidApiClient propertyVoidApiClient,
            IPropertyDetailViewApiClient propertyDetailViewApiClient,
            IPropertyDocumentApiClient propertyDocumentApiClient,
            IRepairApiClient repairApiClient,
            ITenantApiClient tenantApiClient,
            ITenantHistoryViewApiClient tenantHistoryViewApiClient
            ) 
        {
            _propertyApiClient = propertyApiClient;
            _propertyVoidApiClient = propertyVoidApiClient;
            _propertyDetailViewApiClient = propertyDetailViewApiClient;
            _propertyDocumentApiClient = propertyDocumentApiClient;
            _repairApiClient = repairApiClient;
            _tenantApiClient = tenantApiClient;
            _tenantHistoryViewApiClient = tenantHistoryViewApiClient;
        }

        public async Task<AssetModel> GetAsset(int assetId)
        {
            return await _propertyApiClient.GetAsset(assetId);
        }

        public async Task<PropertySearchModel> GetProperties(PropertySearchModel model)
        {
            return await _propertyApiClient.GetProperties(model);
        }

        public async Task<PropertyDto> GetProperty(int id)
        {
            return await _propertyApiClient.GetProperty(id);
        }

        public async Task<PropertyModel> GetPropertyDetails(string propertyCode)
        {
            return await _propertyApiClient.GetPropertyDetails(propertyCode);
        }

        public async Task<VanLocationSearchModel> GetVanLocations(VanLocationSearchModel model)
        {
            return await _propertyApiClient.GetVanLocations(model);
        }

        public async Task<PropertyVoidSearchModel> GetPropertyVoids(PropertyVoidSearchModel model)
        {
            return await _propertyVoidApiClient.GetPropertyVoids(model);
        }

        public async Task<PropertyVoidViewSearchModel> GetPropertyVoidViews(PropertyVoidViewSearchModel model)
        {
            return await _propertyVoidApiClient.GetPropertyVoidViews(model);
        }

        public async Task<PropertyDocumentSearchModel> GetPropertyDocuments(PropertyDocumentSearchModel model)
        {
            return await _propertyDocumentApiClient.GetPropertyDocuments(model);
        }

        public async Task<PropertyDocumentDto> GetPropertyDocument(int id)
        {
            return await _propertyDocumentApiClient.GetPropertyDocument(id);
        }

        public async Task<PropertyDocumentDto> PostPropertyDocument(PropertyDocumentDto model)
        {
            return await _propertyDocumentApiClient.PostPropertyDocument(model);
        }

        public async Task<PropertyDocumentDto> PutPropertyDocument(int id, PropertyDocumentDto model)
        {
            return await _propertyDocumentApiClient.PutPropertyDocument(id,model);
        }

        public async Task<HttpResponseMessage> DeletePropertyDocument(int id)
        {
            return await _propertyDocumentApiClient.DeletePropertyDocument(id);
        }

        public async Task<RepairSearchModel> GetRepairs(RepairSearchModel model)
        {
            return await _repairApiClient.GetRepairs(model);
        }

        public async Task<TenantSearchModel> GetTenants(TenantSearchModel model)
        {
            return await _tenantApiClient.GetTenants(model);
        }

        public async Task<TenantDto> GetTenantByTenantCode(string tenantCode)
        {
            return await _tenantApiClient.GetTenantByTenantCode(tenantCode);
        }

        public async Task<TenantDto> GetTenant(int id)
        {
            return await _tenantApiClient.GetTenant(id);
        }

        public async Task<TenantDto> PostTenant(TenantDto model)
        {
            return await _tenantApiClient.PostTenant(model);
        }

        public async Task<TenantDto> PutTenant(int id, TenantDto model)
        {
            return await _tenantApiClient.PutTenant(id,model);
        }

        public async Task<TenantHistoryViewSearchModel> GetTenantHistoryViews(TenantHistoryViewSearchModel model)
        {
            return await _tenantHistoryViewApiClient.GetTenantHistoryViews(model);
        }

        public async Task<PropertyDetailViewSearchModel> GetPropertyDetailViews(PropertyDetailViewSearchModel model)
        {
            return await _propertyDetailViewApiClient.GetPropertyDetailViews(model);
        }

        public async Task<PropertyDetailViewDto> GetPropertyDetailView(string propertyCode)
        {
            return await _propertyDetailViewApiClient.GetPropertyDetailView(propertyCode);
        }
    }
}