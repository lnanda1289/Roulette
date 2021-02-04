using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public class Roulette
    {
        public int Id { get; set; }
        public bool Open { get; set; }
        public List<BetDto> Bets { get; set; }
        public  int WinNumber { get; set; }
        public String WinColour { get; set; }
    }
}
