using CognitiveAIApis.Models;
using NLayerApp.CognitiveAIApis.Infrastructure;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NLayerApp.CognitiveAIApis.Services.Models;
using System.Security;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;

namespace NLayerApp.CognitiveAIApis.Services
{
    public class FaceApis
    {

        private readonly string _endpointUri;
        private readonly string _version;
        private readonly string _subscriptionKey;
        private readonly Dictionary<string, string> _headers; 
        public FaceApis(string endpointUri, string version, string subscriptionKey)
        {
            _endpointUri = $"{endpointUri}/face";
            _version = version;
            _subscriptionKey = subscriptionKey;
            _headers = new Dictionary<string, string>()
                .with("Accept", "application/json");
        }

        public FaceApis(ApiCredential credential): this(credential.Endpoint, credential.Version, credential.SubscriptionKey)
        {

        }
        public async Task<object> DetectFacesAsync(object objectToProcess, 
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
                    .WithOperationPath("detect")
                    .WithSubscriptionKey(_subscriptionKey)
                    .WithHeaders(_headers)
                    .WithParameters(
                        parameters
                            .InitializeIfNull())
                    .WithContentType("application/octet-stream")
                    .WithPayload(bytes));

                return await apiRequest
                        .ProcessRequest<byte[], List<DetectedFace>>(); 
            }
        }
        public async Task<object> DetectFacesFromUrlAsync(object objectToProcess, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("detect")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(
                    parameters
                        .InitializeIfNull())
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest.ProcessRequest<object, List<DetectedFace>>(); 
        }

        public async Task<ResponseWrapper<List<DetectedFace>>> DetectFacesWithUrlAsync(object objectToProcess, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("detect")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(
                    parameters
                        .InitializeIfNull())
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest
                    .ProcessRequest<object, List<DetectedFace>>();
        }

    }
}
