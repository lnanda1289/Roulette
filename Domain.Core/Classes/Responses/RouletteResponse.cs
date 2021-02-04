using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public class RouletteResponse
    {
        public int WinNumber { get; set; }
        public string WinColour { get; set; }
        public List<BetDto> Winners { get; set; }

    }
}
