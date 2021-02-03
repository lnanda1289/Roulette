using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Core;

namespace Domain.Business
{
    public class RouletteBusiness: IRouletteBusiness
    {
        private readonly IRouletteCache _rouletteCache;

        public RouletteBusiness(IRouletteCache rouletteCache)
        {
            _rouletteCache = rouletteCache ?? throw new ArgumentException(nameof(rouletteCache));
        }
        public int CreateRulette(RouletteDto roulette)
        {
            this._rouletteCache.Save<RouletteDto>(roulette);
            return 1;
        }
    }
}
