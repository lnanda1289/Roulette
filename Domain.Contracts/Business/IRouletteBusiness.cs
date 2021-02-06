using Domain.Core;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IRouletteBusiness
    {
        public int CreateRulette();
        public bool OpenRulette(int id);
        public RouletteResponse CloseRulette(int id);
        public ICollection<Roulette> GetAllRoulettes();
        public int CreateBet(BetDto betDto);
    }
}
