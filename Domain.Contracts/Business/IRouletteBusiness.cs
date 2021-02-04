using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{ 
    public interface IRouletteBusiness
    {
        public int CreateRulette(RouletteDto roulette);
        public int OpenRulette(string id);
        public RouletteResponse CloseRulette(string id);
        public RouletteDto GetAllRoulettes();
    }
}
