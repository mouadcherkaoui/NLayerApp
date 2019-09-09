using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models
{
    public class PersonGroupResponse
    {
        public string personGroupId { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
        public string recognitionModel { get; set; }
    }
}
