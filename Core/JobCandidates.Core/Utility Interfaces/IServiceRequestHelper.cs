using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Core.Utility_Interfaces
{
    public interface IServiceRequestHelper
    {
        Task<string> GetHttpResponse(string requestUri = null);
    }
}
