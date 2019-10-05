namespace CRM.Entity.Settings
{
   public class IdentitySettings
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SignedOutRedirectUri { get; set; }
    }
}
