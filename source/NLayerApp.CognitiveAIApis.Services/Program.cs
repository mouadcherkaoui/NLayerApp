using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rest;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NLayerApp.CognitiveAIApis.Services;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using NLayerApp.CognitiveAIApis.Infrastructure;
using static NLayerApp.CognitiveAIApis.Services.CustomVisionApis;
using CognitiveAIApis.Models.CustomVision;
using NLayerApp.CognitiveAIApis.Services.Models;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using NLayerApp.CognitiveAIApis.Services;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;

namespace NLayerApp.CognitiveAIApis.Services
{
    class Program
    {
        private const string subscriptionKey = "************"; 

        private const string _endpoint =
            "https://westus2.api.cognitive.microsoft.com";

        private const string localImagePath = @"Images/image.jpg";

        private const string remoteImageUrl =
            "http://images5.fanpop.com/image/photos/26900000/Nicolas-Cage-nicolas-cage-26969804-2069-2560.jpg";

        static async Task Main(string[] args)
        {
            var searchApis = new SearchApis(subscriptionKey);
            var searchResult = await searchApis.SearchAsync("blazor");

            Console.WriteLine(JsonConvert.SerializeObject(searchResult.ResponseContent));

            var fluentApiTest = new FluentApiDefinition()
               .HasEndpointUri("uri")
               .OfVersion("version")
               .AndSubscriptionKey("subscriptionKey")
               .AndMethod("method")
               .WithContentType("application/json")
               .AndHeaders(default)
               .AndParameters(default);

            using (var client = new WebClient())
            {
                client.Headers.Add("Ocp-Apim-Subscription-Region", "westus");
                client.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                client.Headers.Add("Accept", "application/json");
                client.QueryString.Add("api-version", "3.0");
                client.QueryString.Add("to", "fr");
                client.QueryString.Add("from", "en");

                var json = JsonConvert.SerializeObject(new[] { new { Text = "Text detection test using Translator Apis" } });
                var responseBytes = 
                    await client.UploadDataTaskAsync(new Uri($"https://api.cognitive.microsofttranslator.com/translate"), "POST", Encoding.UTF8.GetBytes(json));
                var responseString = Encoding.UTF8.GetString(responseBytes);
            }

            TranslatorApis translatorApis = new TranslatorApis(subscriptionKey, "westus");
            var translationResult = await
                new[] { new { Text = "Text detection test using Translator Apis" } }
                    .PipeTo(p => translatorApis.Detect(p));

            Console.WriteLine(translationResult);

            CustomVisionApis visionApis = 
                new CustomVisionApis(_endpoint, "v3.0",  subscriptionKey, trainingKey: "5db8186f847f43079668979adf24f8f3");
            var createProjectResult = await visionApis.CreateProjectAsync(new { name = "testProject" });

            var projectId = createProjectResult.ResponseContent?.id;

            dynamic tagResult0 = await visionApis.CreateTagAsync(new { projectId = projectId, tagName = "fork" });
            dynamic tagResult1 = await visionApis.CreateTagAsync(new { projectId = projectId, tagName = "scissor" });

            tagResult0 = tagResult0.ResponseContent;
            tagResult1 = tagResult1.ResponseContent;

            var requestObject = new CreateImagesRequest()
            {
                images = new List<ImageUrl>
                {
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_1.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_2.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_3.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_4.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_5.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_6.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_7.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_8.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_9.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_10.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/fork/fork_11.jpg?raw=true",
                        tagIds = new List<string>{ tagResult0?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_1.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_2.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_3.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_4.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_5.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_6.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_7.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_8.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_9.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_10.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    },
                    new ImageUrl
                    {
                        url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_11.jpg?raw=true",
                        tagIds = new List<string>{ tagResult1?.id}
                    }
                }
            };

            var tagIds = requestObject.images.Select(i => i.tagIds).Aggregate((l1, l2) => l1.Concat(l2).ToList());

            var createImagesResult = await visionApis.CreateImagesFromUrlsAsync(new { projectId = projectId, requestObject = requestObject });

            var trainingResult = await visionApis.TrainProjectAsync(new { projectId = projectId });

            var iterations = await visionApis.GetIterations(new { projectId = projectId });

            var updateIterationResult = await visionApis.UpdateIteration(new { projectId = projectId, iterationId = iterations?.ResponseContent?[0].id, iteration = new { isDefault = true } });

            var quicktest = await visionApis.QuickTestImageWithUrlAsync(iterations?.ResponseContent?[0].id, projectId, new { url = "https://github.com/Azure/LearnAI-Bootcamp/blob/master/lab01.3_customvision02/Resources/Starter/CustomVision.Sample/Images/scissors/scissors_15.jpg?raw=true" });
        }

        static async Task WaitCallLimitPerSecondAsync()
        {
            const int callLimitPerSecond = 10;
            Queue<DateTime> _timeStampQueue = new Queue<DateTime>(callLimitPerSecond);
            Monitor.Enter(_timeStampQueue);
            try
            {
                if (_timeStampQueue.Count >= callLimitPerSecond)
                {
                    TimeSpan interval = DateTime.UtcNow - _timeStampQueue.Peek();
                    if (interval < TimeSpan.FromSeconds(1))
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1) - interval);
                    }
                    _timeStampQueue.Dequeue();
                }
                _timeStampQueue.Enqueue(DateTime.UtcNow);
            }
            finally
            {
                Monitor.Exit(_timeStampQueue);
            }
        }        
    }
}
