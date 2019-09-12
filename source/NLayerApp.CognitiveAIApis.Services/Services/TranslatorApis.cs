using NLayerApp.CognitiveAIApis.Infrastructure;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;
using NLayerApp.CognitiveAIApis.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.CognitiveAIApis.Services
{
    public class TranslatorApis
    {
        private readonly string _endpointUri = "https://api.cognitive.microsofttranslator.com";
        private readonly Dictionary<string, string> _headers;
        public TranslatorApis(string subscriptionKey, string region = "")
        {
            _headers = new Dictionary<string, string>()
                .with("Ocp-Apim-Subscription-Key", subscriptionKey)
                .with("Ocp-Apim-Subscription-Region", region);
        }

        public async Task<ResponseWrapper<object>> Detect(dynamic objectToProcess, Dictionary<string, string> parameters = null)
        {
            var requestDefinition = new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithHeaders(_headers)
                .WithContentType("application/json")
                .WithMethod("POST")
                .WithOperationPath("detect")
                .WithParameters(
                    parameters
                        .InitializeIfNull()
                        .with("api-version", "3.0"))
                .WithPayload<object[]>(objectToProcess);
            return await requestDefinition.ProcessRequest<object[], object>();
        }
    }
}
