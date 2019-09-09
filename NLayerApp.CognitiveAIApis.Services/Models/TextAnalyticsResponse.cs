using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models
{
    public class TextAnalyticsResponse<TResult>
    {
        public List<TResult> Documents { get; set; }
    }
}
