using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class Prediction
    {
        public double probability { get; set; }
        public string tagId { get; set; }
        public string tagName { get; set; }
        public BoundingBox boundingBox { get; set; }
    }
}
