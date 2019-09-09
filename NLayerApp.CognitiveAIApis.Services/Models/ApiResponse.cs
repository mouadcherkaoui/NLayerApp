using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models
{
    public class ApiResponse<TResponse>
    {
        public string Status { get; set; }
        public TResponse Response { get; set; }
    }
}
