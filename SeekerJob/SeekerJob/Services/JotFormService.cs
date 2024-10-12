using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SeekerJob.Services
{
    public class JotFormService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public JotFormService(string apiKey) 
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }
        public async Task<JObject> GetFormDetails(string formId)
        {
            var requestUrl = $"https://api.jotform.com/form/{formId}?apiKey={_apiKey}"; 
            return await GetJObjectAsync(requestUrl);
        }
        private async Task<JObject> GetJObjectAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); 
                var content = await response.Content.ReadAsStringAsync();
                return JObject.Parse(content);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> GetFormHtmlAsync(string formUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(formUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }

    }
}