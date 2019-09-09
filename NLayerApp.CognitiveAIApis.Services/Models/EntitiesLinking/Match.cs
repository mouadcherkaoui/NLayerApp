using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.EntitiesLinking
{
    public class Match
    {
        public string Text { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}
