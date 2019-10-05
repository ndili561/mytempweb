﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;
using CRM.Entity.Settings;
using CRM.WebApiClient.Interface.Employee;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CRM.WebApiClient.HttpClient.Employee
{
    /// <summary>
    /// </summary>
    public class ChatMessageApiClient : BaseClient, IChatMessageApiClient
    {
        public ChatMessageApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
        }

        public async Task<ChatMessageDto> GetChatMessage(int id)
        {
            var url = CRMApiUri + "/ChatMessage/" + id;
            var result = await GetResultFromApi<ChatMessageDto>(url);
            return result;
        }

        public async Task<ChatMessageDto> PostChatMessage(ChatMessageDto model)
        {
            var url = CRMApiUri + "/ChatMessage";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<ChatMessageDto> PutChatMessage(int id,ChatMessageDto model)
        {
            var url = CRMApiUri + "/ChatMessage/"+ id;
            var result = await PutRequestToApi(url, model);
            return result;
        }

        public async Task<ChatMessageSearchModel> GetChatMessages(ChatMessageSearchModel model)
        {
            var url = CRMApiUri + "/ChatMessage/GetChatMessages?" + GetFilterString(model);
            var result = await GetOdataResultFromApi(url);
            var searchResultCount = 0;
            if (result.Count != null)
                int.TryParse(result.Count.ToString(), out searchResultCount);
            model.TotalRows = searchResultCount;
            model.ChatMessageSearchResult.Clear();
            try
            {
                model.ChatMessageSearchResult.AddRange(result.Items.Select(item => JsonConvert.DeserializeObject<ChatMessageDto>(item.ToString())));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return model;
        }

      

        private string GetFilterString(ChatMessageSearchModel searchModel)
        {
            var filterString = string.Empty;
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FilterText))
                {
                    if (string.IsNullOrWhiteSpace(filterString))
                    {
                        filterString = ODataFilterConstant.Filter + $"contains(Forename,'{searchModel.FilterText}') eq true";
                    }
                    else
                    {
                        filterString += $" or contains(Forename,'{searchModel.FilterText}') eq true";
                    }
                    filterString += $" or contains(Surname,'{searchModel.FilterText}') eq true";
                }
                AddPageSizeNumberAndSortingInFilterString(searchModel, ref filterString);
            }
            return filterString;
        }
    }
}
