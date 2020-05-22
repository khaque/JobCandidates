using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Core.Business_Interfaces;
using JobCandidates.Core.Utility_Interfaces;
using JobCandidates.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;

namespace JobCandidates.Business
{
    public class FindCandidateService : IFindCandidateService
    {
        private readonly IServiceRequestHelper serviceRequestHelper;
        private readonly IJobService jobService;
        private readonly ICandidateService candidateService;
        public FindCandidateService(IServiceRequestHelper serviceRequestHelper, IConfiguration configuration, 
                                    IJobService jobService, ICandidateService candidateService)
        {
            this.serviceRequestHelper = serviceRequestHelper;
            this.jobService = jobService;
            this.candidateService = candidateService;
        }

        /// <summary>
        /// Get Qualified Candidates
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public List<QualifiedCandidate> GetQualifiedCandidates(int jobId)
        {
            var selectedjob = jobService.GetJobs().Result.Find(job => job.JobId == jobId);

            var candidates = candidateService.GetCandidates().Result;

            var qualifiedCandidates = new List<QualifiedCandidate>();

            foreach(var candidate in candidates)
            {
                QualifiedCandidate qualifiedCandidate = new QualifiedCandidate();

                var jobSkillList = selectedjob.Skills.Split(',').ToList();
                var candidateSkillList = candidate.SkillTags.Split(',').ToList();
                
                qualifiedCandidate.JobName = selectedjob.Name;
                qualifiedCandidate.CandidateName = candidate.Name;
                qualifiedCandidate.NumberofSkillMatch = jobSkillList.Count(candidateSkillList.Contains);

                if (qualifiedCandidate.NumberofSkillMatch >= 1)
                {
                    qualifiedCandidates.Add(qualifiedCandidate);
                }

            }

            return qualifiedCandidates.OrderByDescending(candidate => candidate.NumberofSkillMatch)
                                      .ThenBy(candidate => candidate.CandidateName)  
                                      .ToList();
           

        }
    }
}
