using System;
using System.Collections.Generic;
using System.Text;

namespace JobCandidates.Core.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string SkillTags { get; set; }
    }
}
