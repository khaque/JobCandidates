using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobCandidates.Core.Business_Interfaces;

namespace JobCandidates.Controllers
{
    [Route("api")]
    [ApiController]
    public class MatchingCandidateController : ControllerBase
    {
        private readonly IFindCandidateService findCandidateService;
        private readonly IJobService jobService;
        private readonly ICandidateService candidateService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobService"></param>
        /// <param name="candidateService"></param>
        /// <param name="findCandidateService"></param>
        public MatchingCandidateController(IJobService jobService, ICandidateService candidateService,
                                           IFindCandidateService findCandidateService)
        {
            this.findCandidateService = findCandidateService;
            this.jobService = jobService;
            this.candidateService = candidateService;
        }

        /// <summary>
        /// Get All Jobs
        /// </summary>
        /// <returns></returns>
        [HttpGet("jobs")]
        public IActionResult GetJobs()
        {
            try
            {
                return Ok(jobService.GetJobs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Candidate List
        /// </summary>
        /// <returns></returns>
        [HttpGet("candidates")]
        public IActionResult GetCandidates()
        {
            try
            {
                return Ok(candidateService.GetCandidates());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Find Qualified Candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet("qualifiedcandidates/{JobId}")]
        public IActionResult GetQualifiedCandidates(int JobId)
        {
            try
            {
                return Ok(findCandidateService.GetQualifiedCandidates(JobId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}