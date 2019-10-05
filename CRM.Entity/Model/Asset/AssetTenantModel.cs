namespace CRM.Entity.Model.Asset
{
    public class AssetTenantModel
    {
        public string TenantCode { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int AssetId { get; set; }
        public string PropertyCode { get; set; }
        public string TenancyType { get; set; }
        public bool RedFlag { get; set; }
        public bool BlueFlag { get; set; }
    }
}
