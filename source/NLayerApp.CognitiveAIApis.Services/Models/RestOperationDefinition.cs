using NLayerApp.CognitiveAIApis.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;
namespace NLayerApp.CognitiveAIApis.Services.Models
{
    public interface IOperationDefinition
    {
        IOperationDefinition WithEndpoint(string endpoint, string version);
        IOperationDefinition WithEndpoint(string endpoint);
        IOperationDefinition WithSubscriptionKey(string SubscriptionKey);
        IOperationDefinition WithContentType(string ContentType);
        IOperationDefinition WithParameters(Dictionary<string, string> Parameters);
        IOperationDefinition WithHeaders(Dictionary<string, string> headers);
        IOperationDefinition WithVersion(string version);
        IOperationDefinition WithMethod(string method);
        IOperationDefinition WithOperationPath(string operation);
        IOperationDefinition WithOperationSubPath(string operationSubPath);
        IOperationDefinition WithPayload<TPayload>(TPayload objectToProcess);
        Task<ResponseWrapper<TResult>> ProcessRequest<TRequest, TResult>() where TResult : class;
    }


    public class RestOperationDefinition : IOperationDefinition
    {
        private Dictionary<string, object> _innerDict = new Dictionary<string, object>();

        public IOperationDefinition WithSubscriptionKey(string SubscriptionKey)
        {
            WithHeaders("Ocp-Apim-Subscription-Key", SubscriptionKey);
            return this;
        }

        public IOperationDefinition WithContentType(string ContentType)
        {
            this.WithHeaders("Content-Type", ContentType);
            // _innerDict.Add(nameof(ContentType), ContentType);
            return this;
        }

        public IOperationDefinition WithParameters(Dictionary<string, string> Parameters)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Parameters), Parameters);
            return this;
        }

        public IOperationDefinition WithParameters(string key, string value)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Parameters), Parameters.with(key, value));
            return this;
        }

        public IOperationDefinition WithHeaders(Dictionary<string, string> headers)
        {
            Headers = Headers.ExtendWith(headers);
            if (!_innerDict.ContainsKey(nameof(Headers)))
            {
                _innerDict.Add(nameof(Headers), Headers);
            }else 
                _innerDict[nameof(Headers)] = Headers;
            return this;
        }

        public IOperationDefinition WithHeaders(string key, string value)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Headers), Headers.with(key, value));
            //if (!_innerDict.ContainsKey(nameof(Headers)))
            //{
            //    _innerDict.Add(nameof(Headers), Headers);
            //}else 
            //    _innerDict[nameof(Headers)] = Headers;
            return this;
        }

        public IOperationDefinition WithEndpoint(string Endpoint, string Version)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Endpoint_Uri), Endpoint);
            _innerDict.AddOrReplaceKeyValuePair(nameof(Endpoint_Version), Version);
            return this;
        }

        public IOperationDefinition WithEndpoint(string Endpoint)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Endpoint_Uri), Endpoint);
            return this;
        }

        public IOperationDefinition WithVersion(string version)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Endpoint_Version), version);
            return this;
        }

        //private void AddOrReplaceKeyValuePair<TKey, TValue>(ref Dictionary<TKey, TValue> source,
        //    TKey key, TValue value)
        //{
        //    if (source.ContainsKey(key))
        //        source[key] = value;
        //    else
        //    {
        //        source.Add(key, value);
        //    }
        //}

        public async Task<ResponseWrapper<TResult>> ProcessRequest<TRequest, TResult>() where TResult: class
        {
            return await RequestProcessor.ProcessRequest<TResult>(requestDictionary: _innerDict);
        }

        public IOperationDefinition WithMethod(string method)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Operation_Method), method);
            return this;
        }

        public IOperationDefinition WithOperationPath(string operation)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Operation_Path), operation);
            return this;
        }

        public IOperationDefinition WithOperationSubPath(string operationSubPath)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Operation_SubPath), operationSubPath);
            return this;
        }

        public IOperationDefinition WithPayload<TPayload>(TPayload objectToProcess)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(RequestObject), objectToProcess);
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
