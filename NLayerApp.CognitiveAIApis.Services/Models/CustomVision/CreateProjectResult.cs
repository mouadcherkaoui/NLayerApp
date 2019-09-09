using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.CustomVision
{
    public class CreateProjectResult
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object settings { get; set; }
        //"settings": {
        //    "domainId": "string",
        //    "classificationType": "Multiclass",
        //    "targetExportPlatforms": [
        //      "CoreML"
        //    ]
        //        },
        public string created { get; set; }
        public string lastModified { get; set; }
        public string thumbnailUri { get; set; }
        public bool drModeEnabled { get; set; }
    }
}
