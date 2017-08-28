using System;
using BlackJack.Core.Managers;
using System.Collections.Generic;
using BlackJack.Core.Models;
using BlackJack.Core;

namespace BlackJack
{
    public class Lobby
    {
        private GameManager _game;
        private PlayerManager _player;

        public bool IsBurnt => _game.IsBurnt;

        public bool IsBlackJack => _game.IsBlackJack;

        public void RegisterPlayer()
        {
            var name = ReadName();
            var cash = ReadCash();
            _player = new PlayerManager(name, cash);
        }

        private string ReadName()
        {
            Console.WriteLine("Input your name and press 'Enter' :");
            return Console.ReadLine();
        }

        private decimal ReadCash()
        {
            while (true)
            {
                Console.WriteLine("Input your cash size and press 'Enter' :");

                var input = Console.ReadLine();

                if (Decimal.TryParse(input, out decimal cash))
                {
                    return cash;
                }

                Console.WriteLine("Error. Input decimal number, like 250 or 200,5");
            }
        }

        public void ChooseGame()
        {
            var games = new List<GameManager>
            {
                new GameManager(1, 10),
                new GameManager(10, 100),
                new GameManager(100, 1000)
            };

            while (true)
            {
                Console.WriteLine("0 - Game on 1 - 10.");
                Console.WriteLine("1 - Game on 10 - 100.");
                Console.WriteLine("2 - Game on 100 - 1000.");

                var input = Console.ReadLine();

                if (Int32.TryParse(input, out int answer) && answer < 3 && answer >= 0)
                {
                    _game = games[answer];
                    _game.TakePlace(_player);
                    return;
                }

                Console.WriteLine("Error.");
            }
        }

        public bool MakeBet()
        {
            var min = _game.Min;
            var max = _game.Max;

            if (_player.Cash < min)
            {
                Console.WriteLine("Not enough money.");
                return false;
            }

            if (_player.Cash < max)
            {
                max = _player.Cash;
            }

            while (true)
            {
                Console.WriteLine($"Make a bet. [{min}, {max}]");
                var input = Console.ReadLine();
                if (Decimal.TryParse(input, out decimal money) && money >= min && money <= max)
                {
                    money = _player.Give(money);
                    _game.MakeBet(money);
                    return true;
                }
                Console.WriteLine("Error.");
            }
        }

        public void Deal()
        {
            _game.Deal();

            var cards = _game.DealerCards;
            PrintCards(cards, "Dealer");
            Console.WriteLine($"Total {_game.DealerScore} points.");
            Console.WriteLine();
        }

        public void Play()
        {
            while (_game.IsContinue)
            {
                var cards = _game.PlayerCards;
                PrintCards(cards, _player.Name);
                Console.WriteLine($"Total {_game.PlayerScore} points.");
                Console.WriteLine();

                var turns = _game.GetTurns();

                if (turns == AvailableTurns.Stand)
                {
                    _game.TakeTurn(Turn.Stand);
                    return;
                }

                PrintTurns(turns);
                var turn = GetAnswer();
                _game.TakeTurn(turn);
            }
        }

        private void PrintCards(IEnumerable<Card> cards, string introduce)
        {
            Console.WriteLine($"{introduce} have : ");
            foreach (var card in cards)
            {
                Console.WriteLine(card);
            }
        }

        private void PrintTurns(AvailableTurns turns)
        {
            if ((turns & AvailableTurns.Hit) == AvailableTurns.Hit)
            {
                Console.WriteLine("1 - Hit");
            }

            if ((turns & AvailableTurns.Stand) == AvailableTurns.Stand)
            {
                Console.WriteLine("2 - Stand");
            }

            if ((turns & AvailableTurns.Double) == AvailableTurns.Double)
            {
                Console.WriteLine("3 - Double");
            }

            if ((turns & AvailableTurns.Split) == AvailableTurns.Split)
            {
                Console.WriteLine("4 - Split");
            }

            if ((turns & AvailableTurns.Insurance) == AvailableTurns.Insurance)
            {
                Console.WriteLine("5 - Insurance");
            }
        }

        private Turn GetAnswer()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out int answer) && answer < 6 && answer > 0)
                {
                    return (Turn)answer;
                }

                Console.WriteLine("Error. Chose your turn.");
            }
        }

        public void Dealer()
        {
            var cards = _game.DealerCards;
            PrintCards(cards, "Dealer");
            Console.WriteLine($"Total {_game.DealerScore} points.");
            while (_game.DealerPlays)
            {
                var card = _game.DealerTurn();
                Console.WriteLine(card);
                Console.WriteLine($"Score - {_game.DealerScore}");
            }
        }

        public void Winning()
        {
            var money = _game.Winning();
            _player.Get(money);

            if (money > 0)
            {
                Console.WriteLine($"You win {money}.");
                return;
            }
            Console.WriteLine("You lose your bet.");

        }
    }
}
