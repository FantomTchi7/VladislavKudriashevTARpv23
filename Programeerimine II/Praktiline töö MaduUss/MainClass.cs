using System.Diagnostics;

namespace Praktiline_töö_MaduUss
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int x = 80;
            int y = 25;
            int wallX;
            int wallY;
            int choice;
            int choice2;
            int foodCounter = 0;
            double moveTime = 100;
            string gameOver = "Mäng läbi!";
            string user = "opilane";
            ConsoleColor snakeConsoleColor = ConsoleColor.Green;
            string recordTablePath = @"..\..\..\Resources\RekordiTabel.txt";
            List<string> recordTable = new List<string>(File.ReadAllLines(recordTablePath));
            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (true)
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
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Seinad walls = new Seinad(x, y);
                    walls.Draw();

                    Console.ForegroundColor = snakeConsoleColor;
                    Punkt p = new Punkt(4, 5, '*');
                    Madu snake = new Madu(p, 4, Suunas.RIGHT, x, y);
                    snake.Draw();

                    ToitLooja foodCreator = new ToitLooja(x, y, '$');
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Punkt food = foodCreator.LooToit(snake.pList);
                    food.Draw(food.x, food.y, food.sym);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Cyan;
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Punkt newWall;
                                Random rand = new Random();
                                do
                                {
                                    wallX = rand.Next(1, x - 2);
                                    wallY = rand.Next(1, y - 2);
                                    newWall = new Punkt(wallX, wallY, '#');
                                } while (!walls.IsPositionValid(newWall.x, newWall.y, snake.pList, food));

                                walls.AddDynamicWall(newWall);
                                newWall.Draw(newWall.x, newWall.y, newWall.sym);

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Punkt newFood = foodCreator.LooToit(snake.pList);
                                newFood.Draw(newFood.x, newFood.y, newFood.sym);
                                food = newFood;
                                foodCounter += 1;
                                recordTable.Add($"{user}🗿{foodCounter}");
                                Console.SetCursorPosition(0, 0);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($"Söödud toit: {foodCounter} ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.SetCursorPosition(Console.WindowWidth - 12, 0);
                                TimeSpan timerTime = timer.Elapsed;
                                Console.WriteLine($" Aeg: {timerTime.Minutes:D2}:{timerTime.Seconds:D2}");
                                Console.ForegroundColor = snakeConsoleColor;
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition((x / 2) - (gameOver.Length / 2) - 2, y / 2 - 1);
                    Console.Write(gameOver);
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
                        Console.WriteLine("1. Blue\r\n2. Cyan\r\n3. DarkBlue\r\n4. DarkCyan\r\n5. DarkGray\r\n6. DarkGreen\r\n7. DarkMagenta\r\n8. DarkRed\r\n9. DarkYellow\r\n10. Gray\r\n11. Green\r\n12. Magenta\r\n13. Red\r\n14. White\r\n15. Yellow");
                        choice2 = int.Parse(Console.ReadLine());
                        switch (choice2)
                        {
                            case 1:
                                snakeConsoleColor = ConsoleColor.Blue;
                                break;
                            case 2:
                                snakeConsoleColor = ConsoleColor.Cyan;
                                break;
                            case 3:
                                snakeConsoleColor = ConsoleColor.DarkBlue;
                                break;
                            case 4:
                                snakeConsoleColor = ConsoleColor.DarkCyan;
                                break;
                            case 5:
                                snakeConsoleColor = ConsoleColor.DarkGray;
                                break;
                            case 6:
                                snakeConsoleColor = ConsoleColor.DarkGreen;
                                break;
                            case 7:
                                snakeConsoleColor = ConsoleColor.DarkMagenta;
                                break;
                            case 8:
                                snakeConsoleColor = ConsoleColor.DarkRed;
                                break;
                            case 9:
                                snakeConsoleColor = ConsoleColor.DarkYellow;
                                break;
                            case 10:
                                snakeConsoleColor = ConsoleColor.Gray;
                                break;
                            case 11:
                                snakeConsoleColor = ConsoleColor.Green;
                                break;
                            case 12:
                                snakeConsoleColor = ConsoleColor.Magenta;
                                break;
                            case 13:
                                snakeConsoleColor = ConsoleColor.Red;
                                break;
                            case 14:
                                snakeConsoleColor = ConsoleColor.White;
                                break;
                            case 15:
                                snakeConsoleColor = ConsoleColor.Yellow;
                                break;
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.Clear();
                    RekordiTabel.ShowLeaderboard(recordTable, user, snakeConsoleColor);
                }
                else if (choice == 4)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
