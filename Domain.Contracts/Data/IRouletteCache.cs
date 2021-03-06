﻿using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IRouletteCache
    {
        public void SetListCache<TEntity>(ICollection<TEntity> list, string cacheKey);
        public ICollection<TEntity> GetListCache<TEntity>(string cacheKey);
        public bool AddCache<TEntity>(TEntity entity, string cacheKey);
        public int GetIdCache(string cacheIdRouletteKey);
    }
}
