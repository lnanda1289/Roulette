using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IRouletteCache
    {
        public void Save<TEntity>(TEntity entity);
    }
}
