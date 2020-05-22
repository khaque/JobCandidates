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
    public class JobService : IJobService
    {
        private readonly IServiceRequestHelper serviceRequestHelper;
        private readonly string jobEndPoint;
        public JobService(IServiceRequestHelper serviceRequestHelper, IConfiguration configuration)
        {
            this.serviceRequestHelper = serviceRequestHelper;
            this.jobEndPoint = configuration["JobAdderAPI:JobEndPoint"];
        }

        /// <summary>
        /// Get Job List
        /// </summary>
        /// <returns></returns>
        public async Task<List<Job>> GetJobs()
        {
            var jsonResult = await serviceRequestHelper.GetHttpResponse(jobEndPoint);

            return JsonConvert.DeserializeObject<List<Job>>(jsonResult);
        }
    }
}
