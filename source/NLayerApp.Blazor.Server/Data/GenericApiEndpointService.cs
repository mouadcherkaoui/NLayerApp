using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NLayerApp.Blazor.ServerApp.Data
{
    public class GenericApiEndpointService<TEntity>
    {
        HttpClient _client;
        public GenericApiEndpointService(HttpClient client)
        {
            _client = client;
        }

        public async Task<TEntity[]> GetEntitiesAsync()
        {
            var result = await _client.GetJsonAsync<TEntity[]>($"/api/{typeof(TEntity).Name}s");
            return result;           
        }

        public async Task<TEntity> GetEntityAsync<TKey>(TKey key)
        {
            var result = await _client.GetJsonAsync<TEntity>($"/api/{typeof(TEntity).Name}s/{key}");
            return result;
        }
    }
}
