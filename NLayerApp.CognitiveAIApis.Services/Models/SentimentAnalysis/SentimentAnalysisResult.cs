using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.SentimentAnalysis
{
    public class SentimentAnalysisResult: Document
    {
        public double? Score { get; set; }
    }
}
