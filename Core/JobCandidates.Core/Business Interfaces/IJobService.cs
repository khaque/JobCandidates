using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Core.Models;

namespace JobCandidates.Core.Business_Interfaces
{
    public interface IJobService
    {
        Task<List<Job>> GetJobs();
    }
}
