using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class RouletteCache: IRouletteCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;

        public RouletteCache(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache ?? throw new ArgumentException(nameof(distributedCache));
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public void Save<TEntity>(TEntity entity)
        {
            string serializeObject = JsonConvert.SerializeObject(entity);
            byte[] data = Encoding.UTF8.GetBytes(serializeObject);
            _distributedCache.Set("_Roulette", data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Double.Parse(_configuration["RedisCache:CacheExpiration"]))
            });
        }
    }
}
