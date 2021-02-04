using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class RouletteCache : IRouletteCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;

        public RouletteCache(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache ?? throw new ArgumentException(nameof(distributedCache));
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public void SetCache<TEntity>(TEntity entity, string cacheKey)
        {
            string serializeObject = JsonConvert.SerializeObject(entity);
            byte[] data = Encoding.UTF8.GetBytes(serializeObject);
            _distributedCache.Set(cacheKey, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Double.Parse(_configuration["RedisCache:CacheExpiration"]))
            });
        }

        public TEntity GetCache<TEntity>(string cacheKey)
        {
            TEntity deserializeObject = default;
            var result = _distributedCache.Get(cacheKey);
            if (result != null)
            {
                var bytesAsString = Encoding.UTF8.GetString(result);
                deserializeObject = JsonConvert.DeserializeObject<TEntity>(bytesAsString);
            }
            return deserializeObject;
        }

    }
}
