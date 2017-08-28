
using BlackJack.Core.Models;

namespace BlackJack.Core.Managers
{
    public class PlayerManager
    {
        private Player _player;

        public PlayerManager(string name, decimal cash)
        {
            _player = new Player { Name = name, Cash = cash };
        }

        public decimal Give(decimal money)
        {
            bool enough = (_player.Cash >= money);

            if (!enough)
            {
                return 0;
            }

            _player.Cash -= money;
            return money;
        }

        public void Get(decimal money)
        {
            if (money > 0)
            {
                _player.Cash += money;
            }
        }

        public string Name => _player.Name;

        public decimal Cash => _player.Cash;
    }
}
