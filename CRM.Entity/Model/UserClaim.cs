using Newtonsoft.Json;

namespace CRM.Entity.Model
{
    public class UserClaim
    {
        [JsonProperty("m_issuer")]
        public string Issuer { get; set; }

        [JsonProperty("m_originalIssuer")]
        public string OriginalIssuer { get; set; }

        [JsonProperty("m_type")]
        public string Type { get; set; }

        [JsonProperty("m_value")]
        public string Value { get; set; }

        [JsonProperty("m_valueType")]
        public string ValueType { get; set; }

        [JsonProperty("m_properties")]
        public string Properties { get; set; }
    }
}
