using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class Iteration
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string created { get; set; }
        public string lastModified { get; set; }
        public string trainedAt { get; set; }
        public string projectId { get; set; }
        public bool exportable { get; set; }
        public List<string> exportableTo { get; set; }
        public string domainId { get; set; }
        public string classificationType { get; set; }
        public string trainingType { get; set; }
        public int reservedBudgetInHours { get; set; }
        public string publishName { get; set; }
        public string originalPublishResourceId { get; set; }
    }

}
