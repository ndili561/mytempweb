using System.Text;

namespace CRM.Entity.Model.Customer
{
    public class AddressDto : Entity.BaseDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }
        public bool IsActive { get; set; }
        public string AddressHtml { get { return ToHtmlString(); } }
        public string ToHtmlString()
        {
            const string linebreak = "\n";
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(AddressLine1))
            {
                sb.Append(AddressLine1).Append(linebreak);
            }
            if (!string.IsNullOrWhiteSpace(AddressLine2))
            {
                sb.Append(AddressLine2).Append(linebreak);
            }
            if (!string.IsNullOrWhiteSpace(AddressLine3))
            {
                sb.Append(AddressLine3).Append(linebreak);
            }
            if (!string.IsNullOrWhiteSpace(AddressLine4))
            {
                sb.Append(AddressLine4).Append(linebreak);
            }
            if (!string.IsNullOrWhiteSpace(PostCode))
            {
                sb.Append(PostCode).Append(linebreak);
            }
            return (sb.ToString());
        }
    }
}
