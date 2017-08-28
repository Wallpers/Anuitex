using System;
using BlackJack.Core.Models;

namespace BlackJack.Core.Managers
{
    public class ShoesManager
    {
        private Shoes _shoes;

        public ShoesManager()
        {
            Initilize();
        }

        public ShoesManager(int count)
        {
            Initilize(count);
        }

        private void Initilize(int count = 4)
        {
            _shoes = new Shoes(count);
            Shuffle(1000);
        }

        public Card Take()
        {
            if (_shoes.Cards.Count < 30)
            {
                Initilize();
            }

            var card = _shoes.Cards[0];
            _shoes.Cards.RemoveAt(0);
            return card;
        }

        public void Shuffle(int deep)
        {
            var random = new Random();
            var temp = _shoes.Cards[0];
            var index = random.Next(_shoes.Cards.Count);


            for (int i = 0; i < deep; i++)
            {
                _shoes.Cards[0] = _shoes.Cards[index];
                _shoes.Cards[index] = temp;
                temp = _shoes.Cards[0];

                index = random.Next(_shoes.Cards.Count);
            }
        }
    }
}
