using NLayerApp.CognitiveAIApis.Infrastructure;
using CognitiveAIApis.Models.CustomVision;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;
using NLayerApp.CognitiveAIApis.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.CognitiveAIApis.Services
{
    public class CustomVisionApis
    {
        private readonly string _endpointUri;
        private readonly string _version;
        private readonly string _subscriptionKey;
        private readonly string _trainingKey;
        private readonly Dictionary<string, string> _headers;
        private readonly IOperationDefinition _apiCallDefinition;

        public CustomVisionApis(string endpointUri, string version, string subscriptionKey, string trainingKey = "")
        {
            _endpointUri = $"{endpointUri}/customvision";
            _version = version;
            _subscriptionKey = subscriptionKey;
            _trainingKey = trainingKey;
            _headers =
                new Dictionary<string, string>
                {
                    { "Ocp-Apim-Subscription-Key", _subscriptionKey},
                    { "Accept", "application/json" }
                };
            _apiCallDefinition = new RestOperationDefinition()
                 .WithEndpoint(_endpointUri)
                 .WithVersion(_version)
                 .WithSubscriptionKey(_subscriptionKey)
                 .WithHeaders(_headers
                     .with("Training-Key", _trainingKey));
        }

        public CustomVisionApis(ApiCredential credential, string trainingKey = "") 
            : this(credential.Endpoint, credential.Version, credential.SubscriptionKey, trainingKey) { }

        public async Task<ResponseWrapper<CreateProjectResult>> CreateProjectAsync(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest =  _apiCallDefinition
                .WithMethod("POST")
                .WithOperationPath("training")
                .WithOperationSubPath("projects")
                .WithParameters(
                    parameters
                        .InitializeIfNull()
                        .with("name", "testProject"))
                .WithContentType("application/json")
                .WithPayload(objectToProcess);

            return await apiRequest.ProcessRequest<object, CreateProjectResult>();
        }

        public async Task<ResponseWrapper<CreateTagResult>> CreateTagAsync(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = _apiCallDefinition
                .WithMethod("POST")
                .WithOperationPath("training")
                .WithOperationSubPath($"projects/{objectToProcess.projectId}/tags")
                .WithParameters(
                    parameters
                        .InitializeIfNull()
                        .with("name", (string)objectToProcess.tagName))
                .WithContentType("application/json")
                .WithPayload(objectToProcess);

            return await apiRequest
                    .ProcessRequest<object, CreateTagResult>();
        }

        public async Task<ResponseWrapper<CreateImagesResult>> CreateImagesFromUrlsAsync(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = _apiCallDefinition
                .WithMethod("POST")
                .WithOperationPath("training")
                .WithOperationSubPath($"projects/{objectToProcess.projectId}/images/urls")
                .WithParameters(parameters.InitializeIfNull())
                .WithContentType("application/json")
                .WithPayload(objectToProcess.requestObject);

            return
                await apiRequest
                    .ProcessRequest<CreateImagesRequest, CreateImagesResult>();
        }

        public async Task<CreateImagesResult> CreateImagesAsync(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {

            var requestDictionary = new Dictionary<string, object>()
                {
                    { "Endpoint_Uri", $"{_endpointUri}" },
                    { "Endpoint_Version", _version },
                    { "Operation_Method", "POST" },
                    { "Operation_Path", "training" },
                    { "Operation_SubPath", $"projects/{objectToProcess.projectId}/images/urls" },
                    { "Headers", _headers.with("ContentType", "application/octet-stream") },
                    { "Parameters",
                        new Dictionary<string, string>()
                        {
                            { "name", "testTag" }
                        }
                    },
                    { "RequestObject", objectToProcess }
                };


            return
                await RequestProcessor
                    .ProcessRequest<object, CreateImagesResult>(
                        requestDictionary);
        }

        public async Task<ResponseWrapper<TrainingResult>> TrainProjectAsync(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = _apiCallDefinition
                .WithMethod("POST")
                .WithOperationPath("training")
                .WithOperationSubPath($"projects/{objectToProcess.projectId}/train")
                .WithContentType("application/json")
                .WithPayload(objectToProcess);

            return
                await apiRequest
                    .ProcessRequest<object, TrainingResult>();
        }

        public async Task<ResponseWrapper<List<Iteration>>> GetIterations(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = _apiCallDefinition
                .WithMethod("GET")
                .WithOperationPath("training")
                .WithOperationSubPath($"projects/{objectToProcess.projectId}/iterations")
                .WithContentType("application/json")
                .WithPayload(objectToProcess);

            return
                await apiRequest
                    .ProcessRequest<object, List<Iteration>>();
        }

        public async Task<object> UpdateIteration(dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = _apiCallDefinition
                .WithMethod("PATCH")
                .WithOperationPath("training")
                .WithOperationSubPath($"projects/{objectToProcess.projectId}/iterations/{objectToProcess.iterationId}")
                .WithContentType("application/json")
                .WithPayload(objectToProcess);

            return
                await apiRequest
                    .ProcessRequest<object, object>();
        }

        public async Task<ResponseWrapper<QuickTestResult>> QuickTestImageWithUrlAsync(string iterationId, string projectId, 
            dynamic objectToProcess = null, 
            Dictionary<string, string> parameters = null)
        {
            var apiRequest = _apiCallDefinition                
                .WithMethod("POST")
                .WithOperationPath("training")
                .WithOperationSubPath($"projects/{projectId}/quicktest/url")
                .WithParameters(
                    parameters
                        .InitializeIfNull()
                        .with("iterationId", iterationId))
                .WithContentType("application/json")
                .WithPayload(objectToProcess);

            return
                await apiRequest
                    .ProcessRequest<object, QuickTestResult>();
        }
    }
}
