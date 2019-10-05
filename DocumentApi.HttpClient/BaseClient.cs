using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CRM.Dto.Settings;
using DocumentApi.HttpClient.Extension;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DocumentApi.HttpClient
{
    public abstract class BaseClient
    {
        protected readonly IOptions<HttpClientSettings> HttpClientSettings;
        protected static string DocumentApiUri;
        protected static string DocumentFolder;

        protected BaseClient(IOptions<HttpClientSettings> httpClientSettings)
        {
            HttpClientSettings = httpClientSettings;
            DocumentApiUri = httpClientSettings.Value.DocumentApiUri;
            DocumentFolder = httpClientSettings.Value.DocumentFolder;
        }

        protected static void LogErrorMessage(Exception ex, string customMessage = "")
        {
            var sb = new StringBuilder();
            sb.AppendLine(customMessage);
            while (ex != null)
            {
                sb.AppendLine(ex.Message);
                ex = ex.InnerException;
            }
        }
     
        protected static async Task<List<T>>  GetResultsFromApi<T>(string requestUri)
        {
            var response = await HttpRequestFactory.Get(requestUri);
            return response.ContentAsType<List<T>>();
        }
       
        protected static async Task<T> GetResultFromApi<T>(string requestUri)
        {
            var response = await HttpRequestFactory.Get(requestUri);
            return response.ContentAsType<T>();
        }
        protected static async Task<T> PostRequestToApi<T>(string requestUri,T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var response = await HttpRequestFactory.Post(requestUri, model);
            return response.ContentAsType<T>();
        }
        protected static async Task<T> PutRequestToApi<T>(string requestUri, T model)
        {
            var response = await HttpRequestFactory.Put(requestUri, model);
            return response.ContentAsType<T>();
        }
        protected static async Task<HttpResponseMessage> DeleteRequestToApi(string requestUri)
        {
            return await HttpRequestFactory.Delete(requestUri);
        }
    }
}