using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class MainClass
    {
        public static void Main(string[] args)
        {
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

            while (true)
            {
                if (walls.KasLoob(snake) || snake.KasLoobSaba())
                {
                    break;
                }
                if (snake.Soo(food))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    food = foodCreator.LooToit();
                    food.Draw(food.x, food.y, food.sym);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    snake.Move();
                }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    snake.Juhtnupud(key.Key);
                }
                Thread.Sleep(100);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            walls.Draw();
            Console.SetCursorPosition((x / 2) - (gameOver.Length / 2) - 2, y / 2 - 1);
            Console.WriteLine(gameOver);
            Console.ReadLine();
            Console.SetCursorPosition(x - 1, y - 1);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
