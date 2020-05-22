using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using JobCandidates.Core.Models;
using JobCandidates.Core.Business_Interfaces;
using JobCandidates.Core.Utility_Interfaces;
using JobCandidates.Business;
using Moq;
using Microsoft.Extensions.Configuration;

namespace JobCandidates.Test
{
    public class FindCandidateServiceTestRunner
    {
        private readonly Mock<IJobService> jobService;
        private readonly Mock<ICandidateService> candidateService;
        private readonly IFindCandidateService findCandidateService;
        private readonly Mock<IServiceRequestHelper> serviceRequestHelper;
        private readonly Mock<IConfiguration> configuration;
        private readonly List<Job> jobs;
        private readonly List<Candidate> candidates;

        public FindCandidateServiceTestRunner()
        {
            //arrange
            jobService = new Mock<IJobService>();
            candidateService = new Mock<ICandidateService>();
            serviceRequestHelper = new Mock<IServiceRequestHelper>();
            configuration = new Mock<IConfiguration>();
            jobs = new List<Job>();
            candidates = new List<Candidate>();

            findCandidateService = new FindCandidateService(serviceRequestHelper.Object,configuration.Object, jobService.Object, candidateService.Object);

            var Job = new Job() { JobId = 4, Name = "Head Chef", Company= "Bellile", Skills = "creativity, cooking, ordering, cleanliness, service" };
            jobs.Add(Job);

            var candidate = new Candidate() { CandidateId = 1, Name = "Test", SkillTags = "creativity, cleanliness" };
            candidates.Add(candidate);
            candidate = new Candidate() { CandidateId = 2, Name = "Test1", SkillTags = "cooking" };
            candidates.Add(candidate);
            candidate = new Candidate() { CandidateId = 2, Name = "Test2", SkillTags = "Programming" };
            candidates.Add(candidate);
            candidate = new Candidate() { CandidateId = 3, Name = "Test3", SkillTags = "cooking, ordering" };
            candidates.Add(candidate);

            
        }

        [Fact]
        public void VerifyQualifiedCandidates()
        {
            //act
            jobService.Setup(x => x.GetJobs()).ReturnsAsync(jobs);
            candidateService.Setup(x => x.GetCandidates()).ReturnsAsync(candidates);

            //assert
            var result = findCandidateService.GetQualifiedCandidates(4);

            //result shouldn't be empty
            Assert.NotEmpty(result);
            // There should be two qualified Candidates
            Assert.Equal(2, result.Count);
            // Test2 Candidate shouldn't exists cause Test2 skill doesn't match with Job Skill
            Assert.Null(result.Find(x => x.CandidateName == "Test2"));
        }
    }
}
