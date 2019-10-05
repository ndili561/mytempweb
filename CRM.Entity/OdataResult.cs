namespace CRM.Entity
{
    public class OdataResult
    {
        public object[] Items { get; set; }
        public object NextPageLink { get; set; }
        public object Count { get; set; }
        public string odatacontext { get; set; }
    }
}
