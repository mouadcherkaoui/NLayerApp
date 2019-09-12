using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using NLayerApp.CognitiveAIApis.Infrastructure;

namespace CognitiveAIApis.Models
{
    public class ApiRequestHandler<TRequest, TResponse> : IRequestHandler<ApiRequest<TRequest>, TResponse>
    {
        HttpClient innerClient;
        ApiRequest<TRequest> _apiRequest;
        Func<TRequest, HttpRequestMessage> _postRequestAction = null;
        Func<HttpResponseMessage, TResponse> _preRequestAction = null;

        public ApiRequestHandler(ApiRequest<TRequest> apiRequest, 
            Func<TRequest, HttpRequestMessage> postRequestAction = null, 
            Func<HttpResponseMessage, TResponse> preRequestAction = null)
        {
            _apiRequest = apiRequest;
            _postRequestAction = postRequestAction;
            _preRequestAction = preRequestAction;

        }
        public TResponse HandleRequest(ApiRequest<TRequest> request)
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse> HandleRequestAsync(ApiRequest<TRequest> request)
        {
            return await this.HandleRequestAsync(request, new CancellationToken());
        }

        public async Task<TResponse> HandleRequestAsync(ApiRequest<TRequest> request, CancellationToken cancellationToken)
        {
            var subPath = !String.IsNullOrEmpty(_apiRequest.Path) ? $"/{_apiRequest.Path}" : "";
            var requestUri = $"{_apiRequest.BaseAddress}/{_apiRequest.Version}/{_apiRequest.Service}{subPath}";

            requestUri = _apiRequest.QueryParameters.Count > 0 ? $"{requestUri}?" : requestUri;

            var queryString = _apiRequest.QueryParameters.Count > 0 ? _apiRequest.QueryParameters.Select(i => $"{i.Key}={i.Value}").Aggregate( (i, n) => $"{i}&{n}") : "";
            requestUri = $"{requestUri}{queryString}";

            _postRequestAction?.Invoke(request.Request);

            var requestMessage = _postRequestAction?.Invoke(request.Request) 
                ??  new HttpRequestMessage(_apiRequest.Method, $"{requestUri}{queryString}");

            requestMessage.RequestUri = new Uri(requestUri);
            requestMessage.Method = request.Method;

            foreach(var h in _apiRequest.Headers)
            {
                requestMessage.Headers.Add(h.Key, h.Value);
            }

            using (innerClient = new HttpClient())
            {
                return await ResponseMessageToReturnType(requestMessage, cancellationToken);

                if (request.Request != null && !(request.Request is byte[]))
                {
                    var json = JsonConvert.SerializeObject(request.Request);
                    using (var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json)))
                    {
                        requestMessage.Content = content;
                        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        return await ResponseMessageToReturnType(requestMessage, cancellationToken);
                    }
                }
                else if (request.Request is byte[])
                {
                    var bytes = request.Request as byte[];
                    requestMessage.Content = new ByteArrayContent(bytes);
                    requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    return await ResponseMessageToReturnType(requestMessage, cancellationToken);
                }
                else
                {
                    return await ResponseMessageToReturnType(requestMessage, cancellationToken);
                }
            }
        }

        private async Task<TResponse> ResponseMessageToReturnType(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {

            var responseMessage = await innerClient.SendAsync(requestMessage,
                HttpCompletionOption.ResponseContentRead,
                cancellationToken);

            return _preRequestAction != null 
                ? _preRequestAction.Invoke(responseMessage)
                : default;
        }

        public async Task<TResponse> HandleRequestAsync()
        {
            return await this.HandleRequestAsync(_apiRequest, new CancellationToken());
        }
    }
}
