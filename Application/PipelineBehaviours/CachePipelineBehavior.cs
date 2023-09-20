using System;
using System.Text;
using Application.Dto.Common;
using Application.PipelineBehaviours.Contract;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.PipelineBehaviours
{
    public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CachePipelineBehavior(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache) return await next();

            TResponse response;

            string cacheKey = $"{_cacheSettings.ApplicationName}:${request.CacheKey}";

            var cacheResponse = await _cache.GetAsync(cacheKey, cancellationToken);

            if (cacheResponse != null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cacheResponse));  // JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cacheResponse));
            }
            else
            {
                // Get the response and write to cache
                response = await GetResponseAndWriteToCacheAsync();

            }

            return response;

            async Task<TResponse> GetResponseAndWriteToCacheAsync()
            {
                // Send the requet to next handler 
                response = await next();

                // Once next handler is done 

                if (response != null)
                {
                    var slidingExpiration = request?.SlidingExpiration == null ?
                        TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration)
                        : request.SlidingExpiration;

                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration = DateTime.Now.AddDays(1)
                    };


                    // Options to keep Json object pretty and ignore circular dependency error
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        ReferenceHandler= ReferenceHandler.IgnoreCycles
                        
                    };

                    var serializedData = Encoding.Default
                        .GetBytes(
                            JsonSerializer.Serialize
                            (response, options));

                    await _cache.SetAsync(cacheKey, serializedData, cacheOptions, cancellationToken);
                }

                return response;
            }
        }
    }
}

