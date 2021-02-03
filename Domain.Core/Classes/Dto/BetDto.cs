using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public class BetDto
    {
        public int Id { get; set; }
        public double Stake { get; set; }
        public int Number { get; set; }
        public int Colour { get; set; }
        public int IdRoulette { get; set; }
    }
}
