using System;

namespace BlackJack.Core.Models
{
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public int Value
        {
            get
            {
                if (Rank == Rank.Ace)
                {
                    return 11;
                }

                return Math.Min((int)Rank, 10);
            }
        }

        // Must be in ViewModel.
        public override string ToString()
            => $"{Rank} {Suit}";
    }
}
