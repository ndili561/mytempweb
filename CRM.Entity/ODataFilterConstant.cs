namespace CRM.Entity
{
    public static class ODataFilterConstant
    {
        public static string Expand = "&$expand=";//Expands related entities inline.
        public static string Filter = "&$filter="; // filter Filters the results, based on a Boolean condition.
        public static string Inlinecount = "&$count=true";//Tells the server to include the total count of matching entities in the response. (Useful for server-side paging.)
        public static string Orderby = "&$orderby=";
        public static string Select = "&$select=";// Selects which properties to include in the response.
        public static string Skip = "&$skip=";//skip Skips the first n results.
        public static string Top = "&$top="; //Returns only the first n the results.
    }
}