using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);

            HorisontaalJoon topLine = new HorisontaalJoon(0, 78, 0, '+');
            HorisontaalJoon bottomLine = new HorisontaalJoon(0, 78, 24, '+');

            VertikaalJoon leftLine = new VertikaalJoon(0, 24, 0, '+');
            VertikaalJoon rightLine = new VertikaalJoon(0, 24, 78, '+');

            topLine.Draw();
            bottomLine.Draw();

            leftLine.Draw();
            rightLine.Draw();

            Punkt p = new Punkt(4, 5, '*');
            Madu snake = new Madu(p, 4, Suunas.RIGHT);

            snake.Draw();

            while (true)
            {
                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.Juhtnupud(key.Key);
                }
                Thread.Sleep(100);
                snake.Move();
            }
        }
    }
}
