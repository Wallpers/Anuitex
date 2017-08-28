using System;
using System.Collections.Generic;

namespace BlackJack.Core.Models
{
    public class Shoes
    {
        public List<Card> Cards { get; set; }

        public Shoes(int count)
        {
            if (count < 1 || count > 8)
            {
                throw new ArgumentException("Parameter 'count' must be from 1 to 8.");
            }

            Cards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                Cards.AddRange(new Deck().Cards);
            }
        }
    }
}
