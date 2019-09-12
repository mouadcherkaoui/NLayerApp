using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class BoundingBox
    {
        public double left { get; set; }
        public double top { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }
}
