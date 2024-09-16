namespace Praktiline_töö_MaduUss
{
    class RekordiTabel
    {
        public static List<string> UpdateLeaderboard(List<string> recordTable)
        {
            Dictionary<string, int> leaderboard = new Dictionary<string, int>();
            List<string> updatedLeaderboard = new List<string>();

            foreach (string record in recordTable)
            {
                var splitRecord = record.Split("🗿");
                string username = splitRecord[0];
                int score = int.Parse(splitRecord[1]);

                if (leaderboard.ContainsKey(username))
                {
                    if (score > leaderboard[username])
                    {
                        leaderboard[username] = score;
                    }
                }
                else
                {
                    leaderboard.Add(username, score);
                }
            }
            foreach (var entry in leaderboard)
            {
                updatedLeaderboard.Add($"{entry.Key}🗿{entry.Value}");
            }
            return updatedLeaderboard;
        }

        public static void ShowLeaderboard(List<string> recordTable, string currentUser, ConsoleColor foregroundColor)
        {
            var leaderboard = UpdateLeaderboard(recordTable);
            Console.WriteLine("Leaderboard:");

            foreach (var record in leaderboard)
            {
                var splitRecord = record.Split("🗿");
                string username = splitRecord[0];
                int score = int.Parse(splitRecord[1]);

                if (username == currentUser)
                {
                    Console.ForegroundColor = foregroundColor;
                    Console.WriteLine($"{username} - {score}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"{username} - {score}");
                }
            }
            Console.ReadKey(true);
        }
    }
}