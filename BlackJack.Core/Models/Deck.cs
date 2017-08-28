using System.Collections.Generic;

namespace BlackJack.Core.Models
{
    public class Deck
    {
        private readonly Card[] cards;

        public Deck()
        {
            cards = new Card[52];

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 13; j++)
                {
                    cards[i * 13 + j] = new Card() { Suit = (Suit)i, Rank = (Rank)(j + 2) };
                }
            }
        }

        public IEnumerable<Card> Cards => cards;
    }
}
