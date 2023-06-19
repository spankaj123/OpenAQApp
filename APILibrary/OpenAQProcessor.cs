using APILibrary.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace APILibrary
{
    //Singlton pattern implemented
    public class OpenAQProcessor: IOpenAQProcessor
    {
        private readonly ILogger<OpenAQProcessor> _logger;
        public OpenAQProcessor(ILogger<OpenAQProcessor> logger)
        {
            _logger = logger;
        }

        public async Task<AQResults> LoadAQResults (Dictionary<string, string> APIParams)
        {
            if (APIParams.Count > 0)
            {
                string apiQuerystring = HttpUtility.UrlEncode(string.Join("&", APIParams.Select(kvp => $"{kvp.Key}={kvp.Value}")));
                _logger.LogInformation("Calling API...");
                string url = "";
                AQResults aqResults = null;
                try
                {
                    url = $"https://api.openaq.org/v2/latest?";
                    _logger.LogInformation("Calling API with url", url + apiQuerystring);
                    //We can have caching mechanism here before calling api for faster response
                    using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url + apiQuerystring))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            aqResults = await response.Content.ReadAsAsync<AQResults>();
                            //Store request in sotring mechanism like file or database. Need to understand the requirement for storing api request or need caching mechanism. I can implement caching so same request can be returned from cached object.
                            StoreApiRequest(url);
                        }
                        else
                        {
                            _logger.LogError("API call exception:", response.ReasonPhrase);
                            //Need to understand the purpose of ReplayRequest but just for demo purposes, giving example. 
                            AQResults aqResult = await ReplayRequest(url, apiQuerystring);
                            if (aqResult == null)
                            {
                                throw new Exception(response.ReasonPhrase);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception: failed in method LoadAQResults", ex.Message);
                }
                return aqResults;
            }
            else
            {
                _logger.LogError("Exception: No API Parameters provided");
                throw new Exception("No API Parameters provided");
            }
        }

        private void StoreApiRequest(string requestUrl)
        {
            // Store the api request here 
            _logger.LogInformation("Storing API Request: {Request}", requestUrl);
        }

        private async Task<AQResults> ReplayRequest(string url, string apiQuerystring)
        {
            // Replay the api request here 
            _logger.LogInformation("Replaying API Request: {Request}", url + apiQuerystring);
            AQResults? aqResults = null;
            HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, new StringContent(apiQuerystring));
            if (response.IsSuccessStatusCode)
            {
                aqResults = await response.Content.ReadAsAsync<AQResults>();
                _logger.LogInformation("Replayed API Response: {StatusCode}", response.StatusCode);
            }
            else
            {
                _logger.LogError("API call exception:" + response.ReasonPhrase);
                throw new Exception("Replayed API call failed" + response.ReasonPhrase);
            }
            return aqResults;
        }
        
    }
}
