using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Contracts;
using Domain.Core;

namespace Domain.Business
{
    public class RouletteBusiness : IRouletteBusiness
    {
        private readonly IRouletteCache _rouletteCache;
        private readonly IConfiguration _configuration;
        private readonly string cacheKey;
        private readonly string cacheIdRouletteKey;
        private readonly string cacheIdBetKey;

        public RouletteBusiness(IRouletteCache rouletteCache, IConfiguration configuration)
        {
            _rouletteCache = rouletteCache ?? throw new ArgumentException(nameof(rouletteCache));
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
            cacheKey = _configuration["RedisCache:CacheKey"];
            cacheIdRouletteKey = _configuration["RedisCache:CacheIdRouletteKey"];
            cacheIdBetKey = _configuration["RedisCache:CacheIdBetKey"];
        }
        public int CreateRulette()
        {
            Roulette roulette = new Roulette { Id = GetId(cacheIdRouletteKey), Open = false };
            this._rouletteCache.AddCache<Roulette>(roulette, cacheKey);
            return roulette.Id;
        }

        private int GetId(string cacheIdRouletteKey)
        {
           return this._rouletteCache.GetIdCache(cacheIdRouletteKey);
        }

        public bool OpenRulette(int id)
        {
            Roulette roulette = new Roulette { Id = id, Open = true };
            return EditRoulette(roulette, cacheKey);
        }

        public RouletteResponse CloseRulette(int id)
        {
            RouletteResponse rouletteResponse = PlayRoulette(id);
            Roulette roulette = new Roulette
            {
                Id = id,
                Open = false,
                WinColour = rouletteResponse.WinColour,
                WinNumber = rouletteResponse.WinNumber
            };
            EditRoulette(roulette, cacheKey);
            return rouletteResponse;
        }

        public ICollection<Roulette> GetAllRoulettes()
        {
            return _rouletteCache.GetListCache<Roulette>(cacheKey);
        }

        private RouletteResponse PlayRoulette(int idRoulette)
        {
            Roulette roulette = GetRoulette(idRoulette);
            if (roulette != null)
            {
                RouletteResponse rouletteResponse = new RouletteResponse();
                Random randon = new Random();
                rouletteResponse.WinNumber = randon.Next(36);
                rouletteResponse.WinColour = (rouletteResponse.WinNumber % 2) == 0 ? "RED" : "BLACK";
                rouletteResponse.TotalBets = roulette.Bets.Sum(x => x.Stake);
                rouletteResponse.Winners = GetWiners(roulette, rouletteResponse);
                return rouletteResponse;
            }
            return null;
        }

        private List<WinnerResponse> GetWiners(Roulette roulette, RouletteResponse rouletteResponse)
        {
            List<BetDto> betsWinners = roulette.Bets.Where(y => y.Number == rouletteResponse.WinNumber || y.Colour.Equals(rouletteResponse.WinColour)).ToList<BetDto>();
            if(betsWinners != null && betsWinners.Count > 0)
            {
                List<WinnerResponse> winners = new List<WinnerResponse>();

                foreach (BetDto bet in betsWinners)
                {
                    WinnerResponse winner = new WinnerResponse(bet);
                    winner.Gain = bet.Number == rouletteResponse.WinNumber ? bet.Stake * 5 : bet.Stake * 1.8;
                }
                return winners;
            }
            return null;
        }

        public int CreateBet(BetDto betDto)
        {
            Roulette roulette = GetRoulette(betDto.IdRoulette);
            if (roulette != null && roulette.Open)
            {
                betDto.Id = GetId(cacheIdBetKey);
                betDto.Colour = betDto.Colour.ToUpper();
                if(roulette.Bets == null)
                {
                    roulette.Bets = new List<BetDto>();
                }
                roulette.Bets.Add(betDto);
                EditRoulette(roulette, cacheKey);
            }
            return betDto.Id;
        }

        private bool EditRoulette(Roulette roulette, string cacheKey)
        {
            List<Roulette> roulettes = (List<Roulette>)GetAllRoulettes();
            if (roulettes != null)
            {
                var itemIndex = roulettes.FindIndex(x => x.Id == roulette.Id);
                roulettes[itemIndex] = roulette;
                _rouletteCache.SetListCache<Roulette>(roulettes, cacheKey);
                return true;
            }
            return false;
        }

        private Roulette GetRoulette(int IdRoulette)
        {
            List<Roulette> roulettes = (List<Roulette>)GetAllRoulettes();
            if(roulettes != null)
            {
                return roulettes.FirstOrDefault(x => x.Id == IdRoulette);
            }
            return null;
        }

    }
}
