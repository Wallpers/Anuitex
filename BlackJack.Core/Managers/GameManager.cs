using System;
using BlackJack.Core.Models;
using System.Collections.Generic;

namespace BlackJack.Core.Managers
{
    public class GameManager
    {
        private Game _game;
        private PlaceManager _dealerPlace;
        private PlaceManager _playerPlace;
        private ShoesManager _shoes;

        public GameManager(int min, int max)
        {
            _game = new Game { Min = min, Max = max };
            _playerPlace = new PlaceManager();
            _dealerPlace = new PlaceManager();
            _shoes = new ShoesManager();
        }

        public void TakePlace(PlayerManager player)
        {
            if (_playerPlace.Free)
            {
                _playerPlace.TakePlace(player);
            }
        }

        public void ReleasePlace()
        {
            _playerPlace.ReleasePlace();
        }

        public decimal Min => _game.Min;

        public decimal Max => _game.Max;

        public IEnumerable<Card> PlayerCards => _playerPlace.Cards;

        public int PlayerScore => _playerPlace.Score;

        public IEnumerable<Card> DealerCards => _dealerPlace.Cards;

        public int DealerScore => _dealerPlace.Score;

        public bool IsContinue => !_playerPlace.IsStand;

        public bool DealerPlays => _dealerPlace.Score < 17;

        public bool IsBurnt => _playerPlace.IsBurnt;

        public bool IsBlackJack => _playerPlace.IsBlackJack;

        public void MakeBet(decimal bet)
        {
            _playerPlace.MakeBet(bet);
        }

        public void Deal()
        {
            _playerPlace.Add(_shoes.Take());
            _playerPlace.Add(_shoes.Take());

            _dealerPlace.Add(_shoes.Take());
        }

        public AvailableTurns GetTurns()
        {
            var turns = AvailableTurns.Stand;

            if (_playerPlace.Score < 21)
            {
                turns |= AvailableTurns.Hit;
            }

            var playerCards = _playerPlace.Cards;

            if (playerCards.Count == 2)
            {
                turns |= AvailableTurns.Double;
                if (playerCards[0].Rank == playerCards[1].Rank)
                {
                    turns |= AvailableTurns.Split;
                }
            }

            var dealerCards = _dealerPlace.Cards;

            if (dealerCards[0].Rank == Rank.Ace && playerCards.Count == 2)
            {
                turns |= AvailableTurns.Insurance;
            }

            return turns;
        }

        public void TakeTurn(Turn turn)
        {
            if (turn == Turn.Hit)
            {
                _playerPlace.Add(_shoes.Take());
            }

            if (turn == Turn.Stand)
            {
                _playerPlace.Stand();
            }

            if (turn == Turn.Double)
            {
                _playerPlace.Add(_shoes.Take());

                _playerPlace.Stand();
            }

            if (turn == Turn.Split)
            {
                throw new NotImplementedException();
            }

            if (turn == Turn.Insurance)
            {
                throw new NotImplementedException();
            }
        }

        public decimal Winning()
        {
            if (_playerPlace.IsBlackJack)
            {
                return _playerPlace.Bet * 1.5m;
            }

            if (_playerPlace.IsBurnt)
            {
                return 0;
            }

            if (_dealerPlace.IsBurnt || _playerPlace.Score > _dealerPlace.Score)
            {
                return _playerPlace.Bet * 2m;
            }

            return 0;
        }

        public Card DealerTurn()
        {
            var card = _shoes.Take();
            _dealerPlace.Add(card);
            return card;
        }
    }
}
