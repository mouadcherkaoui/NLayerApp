using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class QuickTestResult
    {
        public string id { get; set; }
        public string project { get; set; }
        public string iteration { get; set; }
        public string created { get; set; }
        public List<Prediction> predictions { get; set; }
    }
}
