using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.LanguageDetection
{
    public class Language
    {
        public string Name { get; set; }
        public string Iso6391Name { get; set; }
        public double? Score { get; set; }
    }

}
