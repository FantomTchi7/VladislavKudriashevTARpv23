using System.Diagnostics;

namespace Praktiline_töö_MaduUss
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int x = 80;
            int y = 25;
            int choice;
            int foodCounter = 0;
            double moveTime = 100;
            string gameOver = "Mäng läbi!";
            string user = "opilane";
            string snakeColor = "DarkGreen";
            string recordTablePath = @"..\..\..\Resources\RekordiTabel.txt";
            List<string> recordTable = new List<string>(File.ReadAllLines(recordTablePath));

            while (true)
            {
                Console.SetWindowSize(x, y);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("1. Play game");
                Console.WriteLine("2. Change username & snake color");
                Console.WriteLine("3. View leaderboard");
                Console.WriteLine("4. Exit");
                Console.WriteLine("");
                Console.WriteLine($"Logged in as {user}");
                try
                {
                    choice = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                }
                catch
                {
                    choice = 0;
                }

                if (choice == 1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Seinad walls = new Seinad(x, y);
                    walls.Draw();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Punkt p = new Punkt(4, 5, '*');
                    Madu snake = new Madu(p, 4, Suunas.RIGHT);
                    snake.Draw();

                    ToitLooja foodCreator = new ToitLooja(x, y, '$');
                    Console.ForegroundColor = ConsoleColor.Green;
                    Punkt food = foodCreator.LooToit();
                    food.Draw(food.x, food.y, food.sym);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Console.SetCursorPosition(0, 0);
                    Console.Write($"Söödud toit: {foodCounter} ");

                    while (true)
                    {
                        if (walls.KasLoob(snake) || snake.KasLoobSaba())
                        {
                            break;
                        }
                        if (stopwatch.ElapsedMilliseconds >= moveTime)
                        {
                            bool hasEaten = snake.Soo(food);
                            if (hasEaten)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                food = foodCreator.LooToit();
                                food.Draw(food.x, food.y, food.sym);
                                foodCounter += 1;

                                recordTable.Add($"{user}🗿{foodCounter}");
                                Console.SetCursorPosition(0, 0);
                                Console.Write($"Söödud toit: {foodCounter} ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                snake.Move();
                            }
                            stopwatch.Restart();
                        }
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            snake.Juhtnupud(key.Key);
                        }
                        Thread.Sleep(10);
                    }
                    File.WriteAllLines(recordTablePath, recordTable);
                    recordTable = RekordiTabel.UpdateLeaderboard(recordTable);
                    File.WriteAllLines(recordTablePath, recordTable);
                    foodCounter = 0;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition((x / 2) - (gameOver.Length / 2) - 2, y / 2 - 1);
                    Console.WriteLine(gameOver);
                    Console.ReadKey(true);
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Enter new username: ");
                    user = Console.ReadLine();
                    if (user != "opilane")
                    {
                        Console.Clear();
                        Console.WriteLine("Pick snake color:");
                        Console.WriteLine("1. White     ");
                        Console.WriteLine("2. Red       ");
                        Console.WriteLine("3. Green     ");
                        Console.WriteLine("4. Blue      ");
                        Console.WriteLine("5. Yellow    ");
                        Console.WriteLine("6. Cyan      ");
                        Console.WriteLine("7. Magenta   ");
                        Console.WriteLine("8. Gray      ");
                        Console.WriteLine("9. Orange    ");
                        Console.WriteLine("10. Purple    ");
                        Console.WriteLine("11. Brown     ");
                        Console.WriteLine("12. Pink      ");
                        Console.WriteLine("13. Violet    ");
                        Console.WriteLine("14. DarkBlue  ");
                        Console.WriteLine("15. DarkGreen ");
                        Console.WriteLine("16. DarkRed   ");
                        Console.WriteLine("17. DarkGray  ");
                        Console.WriteLine("18. LightBlue ");
                        Console.WriteLine("19. LightGreen");
                        Console.WriteLine("20. LightGray ");
                        Console.WriteLine("21. LightCoral");
                        Console.WriteLine("22. LightCyan ");
                        Console.WriteLine("23. Aqua      ");
                        Console.WriteLine("24. Lime      ");
                        Console.WriteLine("25. Turquoise ");
                    }
                }
                else if (choice == 3)
                {
                    RekordiTabel.ShowLeaderboard(recordTable, user);
                }
                else if (choice == 4)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}