using System.Diagnostics;

namespace Praktiline_töö_MaduUss
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            int x = 80;
            int y = 25;
            string gameOver = "Mäng läbi!";
            Console.SetWindowSize(x, y);
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

            int foodCounter = 0;
            double moveTime = 100;
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((x / 2) - (gameOver.Length / 2) - 2, y / 2 - 1);
            Console.WriteLine(gameOver);
            Console.ReadKey(true);
            Console.SetCursorPosition(x - 1, y - 1);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
