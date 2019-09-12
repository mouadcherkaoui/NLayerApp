using CognitiveAIApis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using NLayerApp.CognitiveAIApis.Infrastructure;
using NLayerApp.CognitiveAIApis.Services.Models;
using System.Linq;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;

namespace NLayerApp.CognitiveAIApis.Services
{
    public class ComputerVisionApis
    {
        private readonly string _endpointUri = "https://westus.api.cognitive.microsoft.com";
        private readonly string _subscriptionKey = "8cf15696a50e46d6b5c8b8d14fabeec6";
        private readonly string _version = "v2.0";
        private readonly Dictionary<string, string> _headers;

        public ComputerVisionApis(string endpointUri, string version, string subscriptionKey, string trainingKey)
        {
            _endpointUri = $"{endpointUri}/vision";
            _version = version;
            _subscriptionKey = subscriptionKey;
            _headers = new Dictionary<string, string>()
                .with("Ocp-Apim-Subscription-Key", _subscriptionKey)
                .with("Training-Key", trainingKey ?? "")
                .with("Accept", "application/json");
        }
        public ComputerVisionApis(ApiCredential credential): this(credential.Endpoint, credential.Version, credential.SubscriptionKey, "")
        {
        }

        public async Task<object> AnalyzeImage(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            using (FileStream fileStream = new FileStream(((dynamic)objectToProcess).imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                var bytes = binaryReader.ReadBytes((int)fileStream.Length);

                var apiRequest = (new RestOperationDefinition()
                    .WithEndpoint(_endpointUri)
                    .WithVersion(_version)
                    .WithMethod("POST")
                    .WithOperationPath("analyze")
                    .WithSubscriptionKey(_subscriptionKey)
                    .WithHeaders(_headers)
                    .WithParameters(parameters.with("recognitionModel", "true"))
                    .WithContentType("application/octet-stream")
                    .WithPayload(bytes));

                return await apiRequest.ProcessRequest<byte[], object>();
            }
        }
        public async Task<object> AnalyzeImageWithUrl(object objectToProcess = null,
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("analyze")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(
                    parameters
                        .with("recognitionModel", "true"))
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return await apiRequest.ProcessRequest<object, object>();
        }

        public async Task<object> DescribeImage(dynamic objectToProcess = null, Dictionary<string, string> parameters = null)
        {
            using (FileStream fileStream = new FileStream(objectToProcess.imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                var bytes = binaryReader.ReadBytes((int)fileStream.Length);

                var apiRequest = (new RestOperationDefinition()
                    .WithEndpoint(_endpointUri)
                    .WithVersion(_version)
                    .WithMethod("POST")
                    .WithOperationPath("describe")
                    .WithSubscriptionKey(_subscriptionKey)
                    .WithHeaders(_headers)
                    .WithParameters(parameters)
                    .WithContentType("application/octet-stream")
                    .WithPayload(bytes));

                return await apiRequest.ProcessRequest<byte[], object>();
            }
        }
        public async Task<object> DescribeImageWithUrl(object objectToProcess = null,
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("describe")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(parameters)
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return await apiRequest.ProcessRequest<byte[], object>();
        }

        public async Task<object> DetectObjectsWithUrl(object objectToProcess = null,
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("detect")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(parameters)
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return await apiRequest.ProcessRequest<object, object>();

        }

        public async Task<object> GetAreaOfInterestWithUrl(object objectToProcess = null,
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("areaOfInterest")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(parameters)
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return await apiRequest.ProcessRequest<object, object>();
        }

        public async Task<object> RecognizeTextWithUrl(object objectToProcess = null,
            Dictionary<string, string> parameters = null)
        {
            var requestDictionary = new Dictionary<string, object>()
                {
                    { "Endpoint_Uri", $"{_endpointUri}" },
                    { "Endpoint_Version", _version },
                    { "Operation_Method", "POST" },
                    { "Operation_Path", "recognizeText" },
                    { "Operation_SubPath", "" },
                    { "Headers", _headers
                        .Append(new KeyValuePair<string, string> ("ContentType", "application/json"))
                        .ToDictionary(kv => kv.Key, kv => kv.Value) },
                    { "Parameters", parameters},
                    { "RequestObject", objectToProcess }
                };

            return await RequestProcessor.ProcessRequest<object, object>(requestDictionary);
        }

    }
}
