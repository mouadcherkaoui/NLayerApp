using CognitiveAIApis.Models;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;
using NLayerApp.CognitiveAIApis.Services.Models;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.CognitiveAIApis.Services
{
    public class FaceListApis
    {
        private readonly string _endpointUri;
        private readonly string _subscriptionKey;
        private readonly string _version;
        private readonly Dictionary<string, string> _headers;

        public FaceListApis(string endpointUri, string version, string subscriptionKey)
        {
            _endpointUri = endpointUri;
            _version = version;
            _subscriptionKey = subscriptionKey;
            _headers = new Dictionary<string, string>()
                .with("Accept", "application/json");
        }

        public FaceListApis(ApiCredential credential) : this(credential.Endpoint, credential.Version, credential.SubscriptionKey)
        {

        }
        public async Task<FaceList> CreateFaceListAsync(dynamic objectToProcess = null, Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("PUT")
                .WithOperationPath("facelists")
                .WithOperationSubPath($"{objectToProcess.FaceListId}")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(
                    parameters
                        .InitializeIfNull())
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest
                    .ProcessRequest<object, FaceList>();
        }

        public async Task<object> DeleteFaceListAsync(dynamic objectToProcess = null, Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("DELETE")
                .WithOperationPath("facelists")
                .WithOperationSubPath($"{objectToProcess.faceListId}")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(parameters)
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest
                    .ProcessRequest<object, object>();
        }    

    public async Task<object> AddFaceAsync(dynamic objectToProcess = null, Dictionary<string, string> parameters = null)
        {
            using (FileStream fileStream = new FileStream(objectToProcess.imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                var bytes = binaryReader.ReadBytes((int)fileStream.Length);

                var apiRequest = (new RestOperationDefinition()
                    .WithEndpoint(_endpointUri)
                    .WithVersion(_version)
                    .WithMethod("POST")
                    .WithOperationPath("facelists")
                    .WithOperationSubPath($"{objectToProcess.faceListId}/persistedFaces")
                    .WithSubscriptionKey(_subscriptionKey)
                    .WithHeaders(_headers)
                    .WithParameters(parameters
                        .with("detectionModel", $"{objectToProcess.detectionModel}")
                        .with("targetFace", $"{objectToProcess.targetFace}"))
                    .WithContentType("application/octet-stream")
                    .WithPayload(bytes));

                return
                    await apiRequest
                        .ProcessRequest<object, object>();
            }
        }

        public async Task<object> AddFaceWithUrlAsync(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("POST")
                .WithOperationPath("facelists")
                .WithOperationSubPath($"{objectToProcess.faceListId}/persistedFaces")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(parameters
                    .with("targetFace", (string)objectToProcess.targetFace))
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest
                    .ProcessRequest<object, object>();
        }

        public async Task<object> GetFaceListAsync(dynamic objectToProcess = null, Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("GET")
                .WithOperationPath("facelists")
                .WithOperationSubPath($"{objectToProcess.faceListId}")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(
                    parameters
                        .InitializeIfNull())
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest
                    .ProcessRequest<object, FaceList>();
        }

        public async Task<object> ListFaceListsAsync(dynamic objectToProcess = null, Dictionary<string, string> parameters = null)
        {
            var apiRequest = (new RestOperationDefinition()
                .WithEndpoint(_endpointUri)
                .WithVersion(_version)
                .WithMethod("GET")
                .WithOperationPath("facelists")
                .WithSubscriptionKey(_subscriptionKey)
                .WithHeaders(_headers)
                .WithParameters(
                    parameters
                        .InitializeIfNull())
                .WithContentType("application/json")
                .WithPayload(objectToProcess));

            return
                await apiRequest
                    .ProcessRequest<object, object>();
        }
    }
}
