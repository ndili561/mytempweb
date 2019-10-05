using System;

namespace CRM.DAL.Extension
{
    public static class StringExtension
    {
        public static string ConvertByteArrayToString(this byte[] value)
        {
            return value == null ? string.Empty : Convert.ToBase64String(value);
        }

        public static byte[] ConvertStringToByteArray(this string value)
        {
            return value == null || value.Contains("System") ? null : Convert.FromBase64String(value);
        }
        public static string GetFullName(this string value)
        {
            var valueToReplace = " ";
            if (value == null) return string.Empty;
            if (value.Contains("@"))
            {
                valueToReplace = value.Substring(value.IndexOf("@"));
            }
            var returnValue = string.IsNullOrWhiteSpace(value) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(
                value.Replace(valueToReplace, "").Replace(".", " ").ToLower());
            return returnValue.Length > 40 ? returnValue.Substring(0, 40) : returnValue;
        }
    }
}
