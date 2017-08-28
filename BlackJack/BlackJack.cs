using System;

namespace BlackJack
{
    public class BlackJack
    {
        public static void Application()
        {
            var lobby = new Lobby();
            lobby.RegisterPlayer();

            do
            {
                lobby.ChooseGame();
                lobby.MakeBet();
                lobby.Deal();
                lobby.Play();
                lobby.Dealer();
                lobby.Winning();
                while (true)
                {
                    Console.WriteLine("Do you want play again ? 1 - yes, 2 - no");
                    var again = Console.ReadLine();
                    if (again == "1")
                    {
                        continue;
                    }

                    if (again == "1")
                    {
                        break;
                    }
                    Console.WriteLine("Error.");
                }
            } while (true);
        }
    }
}
