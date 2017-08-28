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
                if (!lobby.MakeBet())
                {
                REPEAT:
                    Console.WriteLine("Change game or quit ? 1 - change, 2 - quit.");
                    var answer = Console.ReadLine();
                    if (answer == "1")
                    {
                        continue;
                    }

                    if (answer == "2")
                    {
                        return;
                    }

                    Console.WriteLine("Error");
                    goto REPEAT;
                }
                lobby.Deal();
                lobby.Play();
                if (!lobby.IsBurnt && !lobby.IsBlackJack)
                {
                    lobby.Dealer();
                }
                
                lobby.Winning();
                while (true)
                {
                    Console.WriteLine("Do you want play again ? 1 - yes, 2 - no");
                    var again = Console.ReadLine();
                    if (again == "1")
                    {
                        break;
                    }

                    if (again == "2")
                    {
                        return;
                    }
                    Console.WriteLine("Error.");
                }
            } while (true);
        }
    }
}
