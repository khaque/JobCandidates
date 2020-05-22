using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JobCandidates.Core.Models;

namespace JobCandidates.Core.Business_Interfaces
{
    public interface ICandidateService
    {
        Task<List<Candidate>> GetCandidates();
    }
}
