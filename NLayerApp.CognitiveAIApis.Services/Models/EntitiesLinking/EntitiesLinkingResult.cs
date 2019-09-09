using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.EntitiesLinking
{
    public class EntitiesLinkingResult : Document
    {
        public List<Entity> Entities { get; set; }
    }
}
