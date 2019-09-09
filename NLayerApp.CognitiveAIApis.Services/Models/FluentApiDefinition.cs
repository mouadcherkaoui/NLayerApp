using System;
using System.Collections.Generic;
using System.Text;
using NLayerApp.CognitiveAIApis.Infrastructure.Helpers;
namespace NLayerApp.CognitiveAIApis.Services.Models
{
    public interface IFluentApiDefinition
    {
        IFluentEndpoint HasEndpointUri(string endpoint);
    }

    public interface IFluentEndpoint
    {
        IWithVersion OfVersion(string apiVersion);
    }

    public interface IWithVersion
    {
        IWithVersionAndSubscriptionKey AndSubscriptionKey(string SubscriptionKey);

    }

    public interface IWithVersionAndSubscriptionKey
    {
        IWithMethod AndMethod(string method);
    }

    public interface IWithMethod
    {
        IWithContentType WithContentType(string ContentType);
        IWithHeaders WithHeaders(Dictionary<string, string> headers);
        IWithParameters WithParameters(Dictionary<string, string> headers);

        TResponse ProcessRequest<TRequest, TResponse>(TRequest request);
    }

    public interface IWithHeaders
    {
        IWithHeaders AndContentType(string ContentType);
        IWithHeaders AndParameters(Dictionary<string, string> headers);
        TResponse ProcessRequest<TRequest, TResponse>();
    }

    public interface IWithParameters
    {
        IWithContentTypeAndParameters AndContentType(string ContentType);
        IWithHeadersAndParameters AndHeaders(Dictionary<string, string> ContentType);

        TResponse ProcessRequest<TRequest, TResponse>();
    }

    public interface IWithContentType
    {
        IWithContentTypeAndParameters AndParameters(Dictionary<string, string> ContentType);
        IWithContentTypeAndHeaders AndHeaders(Dictionary<string, string> ContentType);
        TResponse ProcessRequest<TRequest, TResponse>();
    }

    public interface IWithContentTypeAndHeaders
    {
        IDefinedMethod AndParameters(Dictionary<string, string> headers);
        TResponse ProcessRequest<TRequest, TResponse>();
    }

    public interface IWithContentTypeAndParameters
    {
        IDefinedMethod AndHeaders(Dictionary<string, string> headers);
        TResponse ProcessRequest<TRequest, TResponse>();
    }

    public interface IWithHeadersAndParameters
    {
        IDefinedMethod AndContentType(string contentType);
        TResponse ProcessRequest<TRequest, TResponse>();

    }

    public interface IDefinedMethod
    {
        TResponse ProcessRequest<TRequest, TResponse>();
    }
    public class FluentApiDefinition : IFluentApiDefinition
    {
        Dictionary<string, object> _innerDict = new Dictionary<string, object>();
        public FluentApiDefinition(Dictionary<string, object> requestDict = null)
        {
            _innerDict = requestDict ?? new Dictionary<string, object>();
        }
        public IFluentEndpoint HasEndpointUri(string endpointUri)
        {
            _innerDict.AddOrReplaceKeyValuePair("Endpoint_Uri", endpointUri);
            return new EndpointDefinition(_innerDict);
        }

    }

    public class EndpointDefinition : IFluentEndpoint
    {
        Dictionary<string, object> _innerDict;
        public EndpointDefinition(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }
        public IWithVersion OfVersion(string version)
        {
            _innerDict.AddOrReplaceKeyValuePair("Endpoint_Version", version);
            return new EndpointWithVersionDefinition(_innerDict) { };
        }
        public string EndpointUri { get; set; }
        public string Version { get; set; }
        public string SubscriptionKey { get; set; }

    }

    public class EndpointWithVersionDefinition : IWithVersion
    {
        Dictionary<string, object> _innerDict;
        public EndpointWithVersionDefinition(Dictionary<string, object> innerDict = null)
        {
            _innerDict = innerDict;
        }

        public IWithVersionAndSubscriptionKey AndSubscriptionKey(string SubscriptionKey)
        {
            _innerDict.AddOrReplaceKeyValuePair(nameof(Endpoint_SubscriptionKey), SubscriptionKey);
            return new EndpointWithMethodAndSubscriptionKey(_innerDict);
        }

        private string Endpoint_Uri { get; set; }
        private string Endpoint_Version { get; set; }
        private string Endpoint_SubscriptionKey { get; set; }

    }

    public class EndpointWithMethodAndSubscriptionKey : IWithVersionAndSubscriptionKey
    {
        private Dictionary<string, object> _innerDict;

        public EndpointWithMethodAndSubscriptionKey()
        {
        }

        public EndpointWithMethodAndSubscriptionKey(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }

        public IWithMethod AndMethod(string method)
        {
            _innerDict.AddOrReplaceKeyValuePair("Operation_Method", method);
            return new FluentApiMethod(_innerDict);
        }
    }
    public class FluentApiMethod : IWithMethod
    {
        Dictionary<string, object> _innerDict;
        public FluentApiMethod(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }
        public TResponse ProcessRequest<TRequest, TResponse>(TRequest request)
        {
            throw new NotImplementedException();
        }

        public IWithContentType WithContentType(string ContentType)
        {
            _innerDict.AddOrReplaceKeyValuePair("Headers", ContentType);
            return new MethodWithContentType(_innerDict);
        }

        public IWithHeaders WithHeaders(Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }

        public IWithParameters WithParameters(Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }
    }

    public class MethodWithContentType : IWithContentType
    {
        private Dictionary<string, object> _innerDict;

        public MethodWithContentType(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }

        public IWithContentTypeAndHeaders AndHeaders(Dictionary<string, string> headers)
        {
            _innerDict.AddOrReplaceKeyValuePair("Headers", headers);
            return new MethodWithContentTypeAndHeaders(_innerDict);
        }

        public IWithContentTypeAndParameters AndParameters(Dictionary<string, string> parameters)
        {
            _innerDict.AddOrReplaceKeyValuePair("Headers", parameters);
            return new MethodWithContentTypeAndParameters(_innerDict);
        }

        public TResponse ProcessRequest<TRequest, TResponse>()
        {
            throw new NotImplementedException();
        }
    }

    public class MethodWithContentTypeAndHeaders : IWithContentTypeAndHeaders
    {
        private Dictionary<string, object> _innerDict;

        public MethodWithContentTypeAndHeaders(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }

        public IDefinedMethod AndParameters(Dictionary<string, string> parameters)
        {
            _innerDict.AddOrReplaceKeyValuePair("Parameters", parameters);
            return new MethodDefined(_innerDict);
        }

        public TResponse ProcessRequest<TRequest, TResponse>()
        {
            throw new NotImplementedException();
        }
    }

    public class MethodWithContentTypeAndParameters : IWithContentTypeAndParameters
    {
        private Dictionary<string, object> _innerDict;

        public MethodWithContentTypeAndParameters(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }

        public IDefinedMethod AndHeaders(Dictionary<string, string> headers)
        {
            _innerDict.AddOrReplaceKeyValuePair("Headers", headers);
            return new MethodDefined(_innerDict);
        }

        public TResponse ProcessRequest<TRequest, TResponse>()
        {
            throw new NotImplementedException();
        }
    }

    public class MethodDefined : IDefinedMethod
    {
        private Dictionary<string, object> _innerDict;

        public MethodDefined(Dictionary<string, object> innerDict)
        {
            _innerDict = innerDict;
        }

        public TResponse ProcessRequest<TRequest, TResponse>()
        {
            throw new NotImplementedException();
        }
    }
    public class MyClass
    {
        void test()
        {
            new FluentApiDefinition()
                .HasEndpointUri("uri")
                .OfVersion("version")
                .AndSubscriptionKey("subscriptionKey")
                .AndMethod("method")
                .WithContentType("application/jso")
                .AndHeaders(default)
                .AndParameters(default)
                .ProcessRequest<object, object>();
        }
    }
}
