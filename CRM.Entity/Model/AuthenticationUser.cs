using System.Collections.Generic;
using CRM.Entity.Helper;
using Newtonsoft.Json;

namespace CRM.Entity.Model
{
    public class AuthenticationUser
    {
        public AuthenticationUser()
        {
            Claims = new List<UserClaim>();
        }
        public int ContactId { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string UserName { get; set; }

        [JsonProperty("HasRegistered")]
        public bool HasRegistered { get; set; }

        [JsonProperty("LoginProvider")]
        public string LoginProvider { get; set; }

        [JsonProperty("UserClaims")]
        public List<UserClaim> Claims { get; set; }

        [JsonProperty("role")]
        [JsonConverter(typeof(SingleValueArrayConverter))]
        public string[] Roles { get; set; }

        [JsonProperty("sub")]
        public string UserGuid { get; set; }

        [JsonProperty("ViewNotification")]
        public bool ViewNotification { get; set; }
        [JsonProperty("person_id")]
        public string PersonId { get; set; }
       
      
        private bool? _isActive;
        [JsonProperty("IsActive")]
        public bool IsActive
        {
            get
            {
                return !_isActive.HasValue || _isActive.Value;
            }
            set { _isActive = value; }
        }
    }
}
