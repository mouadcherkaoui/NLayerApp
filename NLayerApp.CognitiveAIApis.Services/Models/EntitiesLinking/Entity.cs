using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.EntitiesLinking
{
    public class Entity
    {
        public Match[] Matches { get; set; }
        public string Name { get; set; }
        public string WikipediaId { get; set; }
        public string WikipediaUrl { get; set; }
        public string WikipediaLanguage { get; set; }
        public Guid BingId { get; set; }
    }
}
