using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Praktiline_töö_MaduUss
{
    internal class Madu : Kujund
    {
        public Suunas direction;

        public Madu(Punkt tail, int length, Suunas Direction)
        {
            direction = Direction;
            pList = new List<Punkt>();
            for (int i = 0; i < length; i++)
            {
                Punkt p = new Punkt(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        public void Move()
        {
            Punkt tail = pList.First();
            pList.Remove(tail);
            Punkt head = hankigeJargminePunkt();
            pList.Add(head);

            tail.Tuhjenda();
            head.Draw(head.x, head.y, head.sym); 
        }

        public Punkt hankigeJargminePunkt()
        {
            Punkt head = pList.Last();
            Punkt nextPoint = new Punkt(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        public void Juhtnupud(ConsoleKey key)
        {
            if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && direction != Suunas.RIGHT)
            {
                direction = Suunas.LEFT;
            }
            else if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && direction != Suunas.LEFT)
            {
                direction = Suunas.RIGHT;
            }
            else if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W) && direction != Suunas.DOWN)
            {
                direction = Suunas.UP;
            }
            else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && direction != Suunas.UP)
            {
                direction = Suunas.DOWN;
            }
        }

        internal bool Soo(Punkt food)
        {
            Punkt head = hankigeJargminePunkt();
            if (head.KasLoob(food))
            {
                food.sym = head.sym;
                pList.Add(food);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool KasLoobSaba()
        {
            var head = pList.Last();
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.KasLoob(pList[i]))
                    return true;
            }
            return false;
        }
    }
}
