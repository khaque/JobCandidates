﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobCandidates.Core.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Skills { get; set; }
    }
}
