using NLayerApp.CognitiveAIApis.Infrastructure;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.CognitiveAIApis.Services.Models
{
    public interface IApiDictionary
    {
        IApiDictionary WithEndpoint(string endpoint, string version);
        IApiDictionary WithEndpoint(string endpoint);
        IApiDictionary WithSubscriptionKey(string SubscriptionKey);
        IApiDictionary WithContentType(string ContentType);
        IApiDictionary WithParameters(Dictionary<string, string> Parameters);
        IApiDictionary WithHeaders(Dictionary<string, string> headers);
        IApiDictionary WithVersion(string version);
        IApiDictionary WithMethod(string method);
        IApiDictionary WithOperationPath(string operation);
        IApiDictionary WithOperationSubPath(string operationSubPath);
        IApiDictionary WithPayload<TPayload>(TPayload objectToProcess);
        Task<ResponseWrapper<TResult>> ProcessRequest<TRequest, TResult>() where TResult : class;
    }

    public class ApiCallDefinition : IApiDictionary
    {
        private Dictionary<string, object> _innerDict = new Dictionary<string, object>();
        public IApiDictionary WithSubscriptionKey(string SubscriptionKey)
        {
            _innerDict.Add(nameof(SubscriptionKey), SubscriptionKey);
            return this;
        }

        public IApiDictionary WithContentType(string ContentType)
        {
            this.WithHeaders(nameof(ContentType), ContentType);
            // _innerDict.Add(nameof(ContentType), ContentType);
            return this;
        }

        public IApiDictionary WithParameters(Dictionary<string, string> Parameters)
        {
            _innerDict.Add(nameof(Parameters), Parameters);
            return this;
        }
        public IApiDictionary WithParameters(string key, string value)
        {
            _innerDict.Add(nameof(Parameters), Parameters.with(key, value));
            return this;
        }

        public IApiDictionary WithHeaders(Dictionary<string, string> headers)
        {
            Headers = Headers.ExtendWith(headers);
            if (!_innerDict.ContainsKey(nameof(Headers)))
            {
                _innerDict.Add(nameof(Headers), Headers);
            }else 
                _innerDict[nameof(Headers)] = Headers;
            return this;
        }
        public IApiDictionary WithHeaders(string key, string value)
        {
            Headers = Headers.with(key, value);
            if (!_innerDict.ContainsKey(nameof(Headers)))
            {
                _innerDict.Add(nameof(Headers), Headers.with(key, value));
            }else 
                _innerDict[nameof(Headers)] = Headers;
            return this;
        }
        public IApiDictionary WithEndpoint(string Endpoint, string Version)
        {
            _innerDict.Add(nameof(Endpoint_Uri), Endpoint);
            _innerDict.Add(nameof(Endpoint_Version), Version);
            return this;
        }

        public IApiDictionary WithEndpoint(string Endpoint)
        {
            _innerDict.Add(nameof(Endpoint_Uri), Endpoint);
            return this;
        }

        public IApiDictionary WithVersion(string version)
        {
            _innerDict.Add("Endpoint_Version", version);
            return this;
        }

        public async Task<ResponseWrapper<TResult>> ProcessRequest<TRequest, TResult>() where TResult: class
        {
            return await RequestProcessor.ProcessRequest<TResult>(requestDictionary: _innerDict);
        }

        public IApiDictionary WithMethod(string method)
        {
            _innerDict.Add("Operation_Method", method);
            return this;
        }

        public IApiDictionary WithOperationPath(string operation)
        {
            _innerDict.Add(nameof(Operation_Path), operation);
            return this;
        }

        public IApiDictionary WithOperationSubPath(string operationSubPath)
        {
            _innerDict.Add(nameof(Operation_SubPath), operationSubPath);
            return this;
        }

        public IApiDictionary WithPayload<TPayload>(TPayload objectToProcess)
        {
            _innerDict.Add(nameof(RequestObject), objectToProcess);
            return this;
        }

        public object RequestObject { get => _innerDict[nameof(RequestObject)]; }
        private string Endpoint_Uri { get => (string)_innerDict[nameof(Endpoint_Uri)]; }
        private string Endpoint_Version { get => (string)_innerDict[nameof(Endpoint_Version)]; }
        private string Operation_Method { get => (string)_innerDict[nameof(Operation_Method)]; }
        private string Operation_Path { get => (string)_innerDict[nameof(Operation_Path)]; }
        private string Operation_SubPath { get => (string)_innerDict[nameof(Operation_SubPath)]; }
        private Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        private Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();
    }
}
