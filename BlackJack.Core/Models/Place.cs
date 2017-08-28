using System.Collections.Generic;

namespace BlackJack.Core.Models
{
    public class Place
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        public decimal Bet { get; set; } = 0;
        public bool IsSplited { get; set; } = false;
        public bool IsInsurance { get; set; } = false;
        public bool IsStand { get; set; } = false;
    }
}
