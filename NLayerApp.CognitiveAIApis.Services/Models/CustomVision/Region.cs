using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class Region
    {
        public string regionId { get; set; }
        public string tagName { get; set; }
        public string created { get; set; }
        public string tagId { get; set; }
        public double left { get; set; }
        public double top { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }
}
