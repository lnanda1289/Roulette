namespace Domain.Core
{
    public class WinnerResponse: BetDto
    {
        public double Gain { get; set; }
        public WinnerResponse(BetDto bet)
        {
            this.Colour = bet.Colour;
            this.Id = bet.Id;
            this.IdRoulette = bet.IdRoulette;
            this.Number = bet.Number;
            this.Stake = bet.Stake;
        }
    }
}
