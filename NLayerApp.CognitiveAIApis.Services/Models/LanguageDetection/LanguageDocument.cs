using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.LanguageDetection
{
    public class LanguageDetectionResult: Document
    {
        public List<Language> DetectedLanguages { get; set; }
    }
}
