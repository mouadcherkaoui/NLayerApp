using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveAIApis.Models.KeyPhraseRecognition
{
    public class KeyPhraseRecognitionResult : Document
    {
        public List<string> KeyPhrases { get; set; }
    }
}
