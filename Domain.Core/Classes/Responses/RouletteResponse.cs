using System.Collections.Generic;

namespace Domain.Core
{
    public class RouletteResponse
    {
        public double TotalBets { get; set; }
        public int WinNumber { get; set; }
        public string WinColour { get; set; }
        public List<WinnerResponse> Winners { get; set; }

    }
}
