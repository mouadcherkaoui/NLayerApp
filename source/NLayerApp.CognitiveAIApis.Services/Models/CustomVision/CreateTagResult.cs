using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class CreateTagResult
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public int imageCount { get; set; }
    }
}
