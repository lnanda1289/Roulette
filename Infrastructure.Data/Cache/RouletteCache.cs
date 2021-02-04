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

        public void SetListCache<TEntity>(ICollection<TEntity> list, string cacheKey)
        {
            string serializeObject = JsonConvert.SerializeObject(list);
            byte[] data = Encoding.UTF8.GetBytes(serializeObject);
            _distributedCache.Set(cacheKey, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Double.Parse(_configuration["RedisCache:CacheExpiration"]))
            });
        }

        public ICollection<TEntity> GetListCache<TEntity>(string cacheKey)
        {
            ICollection<TEntity> deserializeColletion = default;
            var result = _distributedCache.Get(cacheKey);
            if (result != null)
            {
                var bytesAsString = Encoding.UTF8.GetString(result);
                deserializeColletion = JsonConvert.DeserializeObject<ICollection<TEntity>>(bytesAsString);
            }
            return deserializeColletion;
        }

        public bool AddCache<TEntity>(TEntity entity, string cacheKey)
        {
            ICollection<TEntity> cacheColletion = GetListCache<TEntity>(cacheKey);
            if(cacheColletion == null)
            {
                cacheColletion = new List<TEntity>();
            }
            cacheColletion.Add(entity);
            SetListCache<TEntity>(cacheColletion, cacheKey);
            return true;
        }

        public int GetIdCache(string cacheIdRouletteKey)
        {
            int id = 0;
            var idcache = GetCache<int>(cacheIdRouletteKey);
            if(idcache != null)
            {
                id = (int)idcache;
            }
            id++;
            SetCache<int>(id, cacheIdRouletteKey);
            return id;
        }

        private void SetCache<TEntity>(TEntity entity, string cacheIdRouletteKey)
        {
            string serializeObject = JsonConvert.SerializeObject(entity);
            byte[] data = Encoding.UTF8.GetBytes(serializeObject);
            _distributedCache.Set(cacheIdRouletteKey, data, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Double.Parse(_configuration["RedisCache:CacheExpiration"]))
            });
        }

        private object GetCache<TEntity>(string cacheIdRouletteKey)
        {
            object deserialize = default;
            var result = _distributedCache.Get(cacheIdRouletteKey);
            if (result != null)
            {
                var bytesAsString = Encoding.UTF8.GetString(result);
                deserialize = JsonConvert.DeserializeObject<TEntity>(bytesAsString);
            }
            return deserialize;
        }
    }
}
