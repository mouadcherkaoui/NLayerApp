using CognitiveAIApis.Models;
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
    public class FaceListsApis
    {
        private const string ApiUri = "https://westus.api.cognitive.microsoft.com";
        private const string SubscriptionKey = "8cf15696a50e46d6b5c8b8d14fabeec6";
        private const string Version = "v1.0";
        private const string Text = "The food was delicious and there were wonderful staff.";
        private const string imageFilePath = "Images/image.jpg";
        public static async Task<FaceList> CreateFaceListAsync(object objectToProcess = null)
        {
            var requestDictionary = new Dictionary<string, object>()
                {
                    { "EndpointUri", $"{ApiUri}/face" },
                    { "Version", "v1.0" },
                    { "Method", "PUT" },
                    { "RequestObject", objectToProcess },
                    { "RequestSubPath",$"{((dynamic)objectToProcess).FaceListId}" },
                    { "RequestPath", "facelists" },
                    { "Headers",
                        new Dictionary<string, string>
                        {
                            { "Ocp-Apim-Subscription-Key", SubscriptionKey },
                            { "ContentType", "application/octet-stream" },
                            { "Accept", "application/json" }
                        }
                    }

                };

            return
                await RequestProcessor
                    .ProcessRequest(
                        requestDictionary,
                        GetPostRequestAction<object>(),
                        GetPreRequestAction<FaceList>());
        }

        public static async Task<object> DeleteFaceListAsync(dynamic objectToProcess = null)
        {
            var requestDictionary = new Dictionary<string, object>()
                {
                    { "EndpointUri", $"{ApiUri}/face" },
                    { "Version", "v1.0" },
                    { "Method", "DELETE" },
                    { "RequestObject", objectToProcess },
                    { "RequestSubPath",$"{((dynamic)objectToProcess).faceListId}" },
                    { "RequestPath", "facelists" },
                    { "Headers",
                        new Dictionary<string, string>
                        {
                            { "Ocp-Apim-Subscription-Key", SubscriptionKey },
                            { "ContentType", "application/octet-stream" },
                            { "Accept", "application/json" }
                        }
                    }
                };

            return
                await RequestProcessor
                    .ProcessRequest(
                        requestDictionary,
                        GetPostRequestAction<object>(),
                        GetPreRequestAction<object>());
        }    

    public static async Task<object> AddFaceAsync(dynamic objectToProcess)
        {
            using (FileStream fileStream = new FileStream(objectToProcess.imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                var bytes = binaryReader.ReadBytes((int)fileStream.Length);

                var requestDictionary = new Dictionary<string, object>()
                {
                    { "EndpointUri", $"{ApiUri}/face" },
                    { "Version", "v1.0" },
                    { "Method", "POST" },
                    { "RequestObject", bytes },
                    { "RequestSubPath",$"{objectToProcess.faceListId}/persistedFaces" },
                    { "RequestPath", "facelists" },
                    { "Headers",
                        new Dictionary<string, string>
                        {
                            { "Ocp-Apim-Subscription-Key", SubscriptionKey },
                            { "ContentType", "application/octet-stream" },
                            { "Accept", "application/json" }
                        }
                    },
                    { "Parameters",
                        new Dictionary<string, string>()
                        {
                            { "detectionModel", objectToProcess.detectionModel },
                            { "targetFace", objectToProcess.targetFace }
                        }
                    }
                };

                return
                    await RequestProcessor
                        .ProcessRequest(
                            requestDictionary,
                            GetPostStreamRequestAction<object>(),
                            GetPreRequestAction<object>());
            }
        }

        public static async Task<object> AddFaceWithUrlAsync(dynamic objectToProcess)
        {
            var requestDictionary = new Dictionary<string, object>()
                {
                    { "EndpointUri", $"{ApiUri}/face" },
                    { "Version", "v1.0" },
                    { "Method", "POST" },
                    { "RequestObject", objectToProcess },
                    { "RequestSubPath",$"{objectToProcess.faceListId}/persistedFaces" },
                    { "RequestPath", "facelists" },
                    { "Headers",
                        new Dictionary<string, string>
                        {
                            { "Ocp-Apim-Subscription-Key", SubscriptionKey },
                            { "ContentType", "application/octet-stream" },
                            { "Accept", "application/json" }
                        }
                    },
                    { "Parameters",
                        new Dictionary<string, string>()
                        {
                            { "targetFace", objectToProcess.targetFace }
                        }
                    }
                };

            return
                await RequestProcessor
                    .ProcessRequest(
                        requestDictionary,
                        GetPostRequestAction<object>(),
                        GetPreRequestAction<object>());
        }

        public static async Task<object> GetFaceListAsync(dynamic objectToProcess = null)
        {
            var requestDictionary = new Dictionary<string, object>()
                {
                    { "EndpointUri", $"{ApiUri}/face" },
                    { "Version", "v1.0" },
                    { "Method", "GET" },
                    { "RequestObject", objectToProcess },
                    { "RequestSubPath",$"{objectToProcess.faceListId}" },
                    { "RequestPath", "facelists" },
                    { "Headers",
                        new Dictionary<string, string>
                        {
                            { "Ocp-Apim-Subscription-Key", SubscriptionKey },
                            { "ContentType", "application/octet-stream" },
                            { "Accept", "application/json" }
                        }
                    }

                };

            return
                await RequestProcessor
                    .ProcessRequest(
                        requestDictionary,
                        GetPostRequestAction<object>(),
                        GetPreRequestAction<FaceList>());
        }

        public static async Task<object> ListFaceListsAsync(dynamic objectToProcess = null)
        {
            var requestDictionary = new Dictionary<string, object>()
                {
                    { "EndpointUri", $"{ApiUri}/face" },
                    { "Version", "v1.0" },
                    { "Method", "GET" },
                    { "RequestObject", objectToProcess },
                    { "RequestSubPath","" },
                    { "RequestPath", "facelists" },
                    { "Headers",
                        new Dictionary<string, string>
                        {
                            { "Ocp-Apim-Subscription-Key", SubscriptionKey },
                            { "ContentType", "application/octet-stream" },
                            { "Accept", "application/json" }
                        }
                    }

                };

            return
                await RequestProcessor
                    .ProcessRequest(
                        requestDictionary,
                        GetPostRequestAction<object>(),
                        GetPreRequestAction<object>());
        }

    private static Func<TRequest, HttpRequestMessage> GetPostRequestAction<TRequest>()
    {
        return new Func<TRequest, HttpRequestMessage>((request) =>
        {
            var json = JsonConvert.SerializeObject(request);
            var byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
            var requestMessage = new HttpRequestMessage() { Content = byteArrayContent };

            return requestMessage;
        });
    }
    private static Func<TRequest, HttpRequestMessage> GetPostStreamRequestAction<TRequest>()
    {
        return (request) =>
        {
            var bytes = request as byte[];
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var requestMessage = new HttpRequestMessage() { Content = byteArrayContent };

            return requestMessage;
        };
    }

    private static Func<HttpResponseMessage, TResponse> GetPreRequestAction<TResponse>()
    {
        return new Func<HttpResponseMessage, TResponse>((responseMessage) =>
        {
            var jsonContent = responseMessage.Content.ReadAsStringAsync().Result;
            var resultToReturn = JsonConvert.DeserializeObject<TResponse>(jsonContent);
            return resultToReturn;
        });
    }
}
}
