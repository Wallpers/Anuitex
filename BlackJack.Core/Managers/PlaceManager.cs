using BlackJack.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Core.Managers
{
    public class PlaceManager
    {
        private Place _place;
        private PlaceManager _split;
        private PlayerManager _player;

        public PlaceManager()
        {
            _place = new Place();
        }

        public void TakePlace(PlayerManager player)
        {
            _player = player;
        }

        public void ReleasePlace()
        {
            _player = null;
        }

        public int Score
        {
            get
            {
                var score = _place.Cards.Sum(card => card.Value);

                if (score <= 21)
                {
                    return score;
                }

                var aces = _place.Cards.Count(card => card.Rank == Rank.Ace);

                while (score > 21 && aces > 0)
                {
                    score -= 10;
                    aces--;
                }
                return score;
            }
        }

        public void Add(Card card)
        {
            _place.Cards.Add(card);
        }

        public void MakeBet(decimal bet)
        {
            _place.Bet = bet;
        }

        public bool Free => _player == null;

        public bool IsSplitted => _split != null;

        public List<Card> Cards => _place.Cards;

        public bool IsStand => _place.IsStand;

        public bool IsBlackJack => _place.Cards.Count == 2 && Score == 21;

        public decimal Bet => _place.Bet;

        public bool IsBurnt => Score > 21;

        public void Stand()
        {
            _place.IsStand = true;
        }
    }
}
