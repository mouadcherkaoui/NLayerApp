using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CognitiveAIApis.Models
{
    public class HttpMethods
    {
    }
    public class ApiRequest<TRequest>
    {
        HttpClient innerClient;
        public ApiRequest()
        {
        }
        public string BaseAddress { get; set; }
        public string Version { get; set; }
        public string Service { get; set; }
        public string SubscriptionKey { get; set; }
        public string Path { get; set; }
        public Dictionary<string, string> QueryParameters { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public TRequest Request { get; set; }
        public HttpMethod Method { get; set; }
    }
}
