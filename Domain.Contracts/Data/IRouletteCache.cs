using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IRouletteCache
    {
        public void SetCache<TEntity>(TEntity entity, string cacheKey);
        public TEntity GetCache<TEntity>(string cacheKey);
    }
}
