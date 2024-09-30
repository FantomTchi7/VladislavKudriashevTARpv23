namespace Football
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            int x = 80;
            int y = 25;
            int playersOnEachTeam = 11;
            Console.SetWindowSize(x, y);
            Console.Clear();
            Stadium stadium = new Stadium(x, y);
            Team homeTeam = new Team("homeTeam");
            Team awayTeam = new Team("awayTeam");
            Game game = new Game(homeTeam, awayTeam, stadium);
            game.Start();

            int homeX = 0, homeY = 0, awayX = 0, awayY = 0, position = 0;
            int goalkeepers = playersOnEachTeam / 11;
            int defenders = playersOnEachTeam * 4 / 11;
            int midfielders = playersOnEachTeam * 4 / 11;
            int forwards = playersOnEachTeam * 2 / 11;

            for (int i = 0; i < playersOnEachTeam; i++)
            {
                if (i < goalkeepers)
                {
                    homeX = x / 9;
                    homeY = (i + 1) * y / (goalkeepers + 1);
                    awayX = x - (x / 9);
                    awayY = (i + 1) * y / (goalkeepers + 1);
                }
                else if (i < goalkeepers + defenders)
                {
                    position = i - goalkeepers;
                    homeX = 2 * x / 9;
                    homeY = (position + 1) * y / (defenders + 1);
                    awayX = x - (2 * x / 9);
                    awayY = (position + 1) * y / (defenders + 1);
                }
                else if (i < goalkeepers + defenders + midfielders)
                {
                    position = i - goalkeepers - defenders;
                    homeX = 4 * x / 9;
                    homeY = (position + 1) * y / (midfielders + 1);
                    awayX = x - (4 * x / 9);
                    awayY = (position + 1) * y / (midfielders + 1);
                }
                else
                {
                    position = i - goalkeepers - defenders - midfielders;
                    homeX = 6 * x / 9;
                    homeY = (position + 1) * y / (forwards + 1);
                    awayX = x - (6 * x / 9);
                    awayY = (position + 1) * y / (forwards + 1);
                }

                homeTeam.AddPlayer(new Player($"Home Player {i + 1}", homeX, homeY));
                awayTeam.AddPlayer(new Player($"Away Player {i + 1}", awayX, awayY));
            }

            while (true)
            {
                game.Move();
                Console.ForegroundColor = ConsoleColor.Red;
                game.Ball.Draw('O', stadium);
                Console.ForegroundColor = ConsoleColor.Green;
                homeTeam.Players.ForEach(player => player.Draw('H', stadium));
                Console.ForegroundColor = ConsoleColor.Blue;
                awayTeam.Players.ForEach(player => player.Draw('A', stadium));
                Thread.Sleep(100);
            }
        }
    }
}
