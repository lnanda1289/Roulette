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
            this._rouletteCache.SetCache<RouletteDto>(roulette, roulette.Id);
            return 1;
        }

        public int OpenRulette(string id)
        {
            RouletteDto roulette = new RouletteDto { Id = id, Open = true };
            this._rouletteCache.SetCache<RouletteDto>(roulette, id);
            return 1;
        }

        public RouletteResponse CloseRulette(string id)
        {
            RouletteResponse rouletteResponse = PlayRoulette();
            RouletteDto roulette = new RouletteDto { Id = id, Open = false };
            this._rouletteCache.SetCache<RouletteDto>(roulette, id);

            return rouletteResponse;
        } 

        public RouletteDto GetAllRoulettes()
        {
            return _rouletteCache.GetCache<RouletteDto>("_Roulette");
        }

        private RouletteResponse PlayRoulette()
        {
            RouletteResponse rouletteResponse = new RouletteResponse();
            Random randon = new Random();
            rouletteResponse.WinNumber = randon.Next(36);
            rouletteResponse.WinColour = (rouletteResponse.WinNumber % 2) == 0 ? "RED" : "BLACK";
            return rouletteResponse;
        }
    }
}
