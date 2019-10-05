using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CRM.Entity.Helper
{
    public static class StringExtension
    {
        public static string GetContentType(this string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public static string GetDocumentType(this string path)
        {
            var types = GetDocumentType();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private static Dictionary<string, string> GetDocumentType()
        {
            return new Dictionary<string, string>
            {
                {".ics", "Calendar"},
                {".txt", "Text"},
                {".pdf", "PDF"},
                {".doc", "Word"},
                {".docx", "Word"},
                {".xls", "Excel"},
                {".xlsx", "Excel"},
                {".png", "Image"},
                {".jpg", "Image"},
                {".jpeg", "Image"},
                {".gif", "Image"},
                {".csv", "CSV"}
            };
        }
        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".ics", "text/calendar"},
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public static string StandarizedForOdata(this string value)
        {
            return value == null ? string.Empty : value.Replace("@odata.count", "Count").Replace("value", "Items");
        }
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
            string valueToReplace = " ";
            if (value.Contains("@"))
            {
                valueToReplace = value.Substring(value.IndexOf("@"));
            }

            var returnValue = string.IsNullOrWhiteSpace(value) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(
                value.Replace(valueToReplace, "").Replace(".", " ").ToLower());
            return returnValue.Length > 40 ? returnValue.Substring(0, 40) : returnValue;
        }
        public static string RemoveSpaces(this string value)
        {
            return value == null ? string.Empty : Regex.Replace(value, @"\s+", "");
        }
    }
}
