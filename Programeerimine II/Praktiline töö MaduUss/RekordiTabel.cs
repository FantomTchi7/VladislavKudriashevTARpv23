using System.Diagnostics;

namespace Praktiline_töö_MaduUss
{
    class RekordiTabel
    {
        public static List<string> UpdateLeaderboard(List<string> recordTable)
        {
            Dictionary<string, (int Score, string Timer)> leaderboard = new Dictionary<string, (int Score, string Timer)>();
            List<string> updatedLeaderboard = new List<string>();

            foreach (string record in recordTable)
            {
                var splitRecord = record.Split("🗿");
                string username = splitRecord[0];
                int score = int.Parse(splitRecord[1]);
                string timer = splitRecord[2];

                if (leaderboard.ContainsKey(username))
                {
                    if (score > leaderboard[username].Score)
                    {
                        leaderboard[username] = (score, timer);
                    }
                }
                else
                {
                    leaderboard.Add(username, (score, timer));
                }
            }

            foreach (var entry in leaderboard)
            {
                updatedLeaderboard.Add($"{entry.Key}🗿{entry.Value.Score}🗿{entry.Value.Timer}");
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
                string timer = splitRecord[2];

                if (username == currentUser)
                {
                    Console.ForegroundColor = foregroundColor;
                    Console.WriteLine($"{username} - {score} in {timer}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"{username} - {score} in {timer}");
                }
            }
            Console.ReadKey(true);
        }
    }
}