using System.Diagnostics;

namespace Praktiline_töö_MaduUss
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int x = 80;
            int y = 25;
            int foodCounter = 0;
            double moveTime = 100;
            string gameOver = "Mäng läbi!";
            string user = "opilane";
            ConsoleColor snakeConsoleColor = ConsoleColor.Green;
            string recordTablePath = @"..\..\..\Resources\RekordiTabel.txt"; // Windows
            // string recordTablePath = @"Resources/RekordiTabel.txt"; // Linux
            List<string> recordTable = new List<string>(File.ReadAllLines(recordTablePath));

            while (true)
            {
                SetupConsole(x, y, user, snakeConsoleColor);
                int choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        PlayGame(ref foodCounter, ref moveTime, x, y, snakeConsoleColor, gameOver, user, recordTablePath, ref recordTable);
                        break;
                    case 2:
                        ChangeUserSettings(ref user, ref snakeConsoleColor);
                        break;
                    case 3:
                        Console.Clear();
                        RekordiTabel.ShowLeaderboard(recordTable, user, snakeConsoleColor);
                        break;
                    case 4:
                        return;
                }
            }
        }

        static void SetupConsole(int x, int y, string user, ConsoleColor snakeConsoleColor)
        {
            Console.SetWindowSize(x, y);
            Console.Clear();
            Console.ForegroundColor = snakeConsoleColor;

            Console.SetCursorPosition(36, 0);
            Console.Write("____");
            Console.SetCursorPosition(35, 1);
            Console.Write("/ . .\\");
            Console.SetCursorPosition(35, 2);
            Console.Write("\\  ---<");
            Console.SetCursorPosition(36, 3);
            Console.Write("\\  /");
            Console.SetCursorPosition(26, 4);
            Console.Write("__________/ /");
            Console.SetCursorPosition(23, 5);
            Console.Write("-=:___________/");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\r\n1. Play game\r\n2. Change username & snake color\r\n3. View leaderboard\r\n4. Exit\r\n");
            Console.Write($"Logged in as {user}");
        }

        static int GetUserChoice()
        {
            return int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int choice) ? choice : 0;
        }
        
        static void PlayGame(ref int foodCounter, ref double moveTime, int x, int y, ConsoleColor snakeConsoleColor, string gameOver, string user, string recordTablePath, ref List<string> recordTable)
        {
            Madu snake = InitializeGame(out Seinad walls, ref foodCounter, x, y, snakeConsoleColor);

            ToitLooja foodCreator = new ToitLooja(x, y, '$');
            Punkt food = foodCreator.LooToit(snake.pList);
            DrawFood(food);

            Stopwatch timer = new Stopwatch();
            timer.Start();

            Stopwatch gameStopwatch = Stopwatch.StartNew();

            Random rand = new Random();

            while (true)
            {
                if (CheckGameOver(snake, walls)) break;

                if (gameStopwatch.ElapsedMilliseconds >= moveTime)
                {
                    UpdateGameStats(foodCounter, timer, x, y);
                    if (snake.Soo(food))
                    {
                        foodCounter++;

                        if (timer.Elapsed.Hours > 0)
                        {
                            recordTable.Add($"{user}🗿{foodCounter}🗿{timer.Elapsed.Hours:D1}h {timer.Elapsed.Minutes:D1}m {timer.Elapsed.Seconds:D1}s");
                        }
                        else if (timer.Elapsed.Minutes > 0)
                        {
                            recordTable.Add($"{user}🗿{foodCounter}🗿{timer.Elapsed.Minutes:D1}m {timer.Elapsed.Seconds:D1}s");
                        }
                        else
                        {
                            recordTable.Add($"{user}🗿{foodCounter}🗿{timer.Elapsed.Seconds:D1}s");
                        }

                        Punkt newWall;
                        do
                        {
                            newWall = new Punkt(rand.Next(1, x - 2), rand.Next(1, y - 2), '#');
                        } while (!walls.IsPositionValid(newWall.x, newWall.y, snake.pList, food));

                        walls.AddDynamicWall(newWall);
                        Console.ForegroundColor = ConsoleColor.Red;
                        newWall.Draw(newWall.x, newWall.y, newWall.sym);
                        
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        DrawFood(food = foodCreator.LooToit(snake.pList));
                    }
                    else
                    {
                        Console.ForegroundColor = snakeConsoleColor;
                        snake.Move();
                    }

                    gameStopwatch.Restart();
                }

                HandleUserInput(snake);
                Thread.Sleep(10);
            }

            timer.Stop();
            SaveLeaderboard(recordTablePath, ref recordTable);
            DisplayGameOverMessage(x, y, gameOver);
        }

        static Madu InitializeGame(out Seinad walls, ref int foodCounter, int x, int y, ConsoleColor snakeConsoleColor)
        {
            Console.Clear();
            foodCounter = 0;

            walls = new Seinad(x, y);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            walls.Draw();

            Punkt snakeStartPoint = new Punkt(4, 5, '*');
            Madu snake = new Madu(snakeStartPoint, 4, Suunas.RIGHT, x, y);
            Console.ForegroundColor = snakeConsoleColor;
            snake.Draw();

            return snake;
        }

        static bool CheckGameOver(Madu snake, Seinad walls)
        {
            return walls.KasLoob(snake) || snake.KasLoobSaba();
        }

        static void HandleUserInput(Madu snake)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                snake.Juhtnupud(key.Key);
            }
        }

        static void UpdateGameStats(int foodCounter, Stopwatch timer, int x, int y)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Söödud toit: {foodCounter} ");

            Console.SetCursorPosition(x - 12, 0);
            Console.Write($" Aeg: {timer.Elapsed:mm\\:ss}");
        }

        static void DrawFood(Punkt food)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            food.Draw(food.x, food.y, food.sym);
        }

        static void SaveLeaderboard(string recordTablePath, ref List<string> recordTable)
        {
            File.WriteAllLines(recordTablePath, recordTable);
        }

        static void DisplayGameOverMessage(int x, int y, string gameOver)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((x / 2) - (gameOver.Length / 2), y / 2 - 1);
            Console.Write(gameOver);
            Console.ReadKey(true);
        }

        static void ChangeUserSettings(ref string user, ref ConsoleColor snakeConsoleColor)
        {
            Console.Clear();
            Console.Write("Enter new username: ");
            user = Console.ReadLine();

            if (user != "opilane")
            {
                Console.Clear();
                Console.WriteLine("Pick snake color:");
                string[] colors = Enum.GetNames(typeof(ConsoleColor));
                for (int i = 0; i < colors.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {colors[i]}");
                }

                if (int.TryParse(Console.ReadLine(), out int colorChoice) && colorChoice >= 1 && colorChoice <= colors.Length)
                {
                    snakeConsoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colors[colorChoice - 1]);
                }
            }
        }
    }
}
