using System;
using System.Collections.Generic;
using System.Text;
using JobCandidates.Core.Utility_Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace JobCandidates.Business
{
    public class ServiceRequestHelper : IServiceRequestHelper
    {
        private readonly string APIUrl;
        public ServiceRequestHelper(IConfiguration configuration)
        {
            APIUrl = configuration["JobAdderAPI:BaseUrl"];
        }
        public async Task<string> GetHttpResponse(string requestUri = null)
        {
            using (var client = new HttpClient())
            {

                if (APIUrl == "")
                {
                    throw new Exception("Destination API URL not found");
                }
                client.BaseAddress = new Uri(APIUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    using (var response = client.GetAsync(requestUri).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            throw new Exception(response.StatusCode + response.ReasonPhrase);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
