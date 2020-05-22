using System;
using System.Collections.Generic;
using System.Text;
using JobCandidates.Core.Business_Interfaces;
using JobCandidates.Core.Utility_Interfaces;
using JobCandidates.Core.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace JobCandidates.Business
{
    public class CandidateService : ICandidateService
    {
        private readonly IServiceRequestHelper serviceRequestHelper;
        private readonly string candidateEndPoint;
        public CandidateService(IServiceRequestHelper serviceRequestHelper, IConfiguration configuration)
        {
            this.serviceRequestHelper = serviceRequestHelper;
            this.candidateEndPoint = configuration["JobAdderAPI:CandidateEndPoint"];
        }

        /// <summary>
        /// Get Candidate List
        /// </summary>
        /// <returns></returns>
        public async Task<List<Candidate>> GetCandidates()
        {
            var jsonResult = await serviceRequestHelper.GetHttpResponse(candidateEndPoint);

            return JsonConvert.DeserializeObject<List<Candidate>>(jsonResult);
        }
    }
}
