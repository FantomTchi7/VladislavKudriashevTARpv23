using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class Punkt
    {
        public int x;
        public int y;
        public char sym;

        public Punkt() { }

        public Punkt(int X, int Y, char Sym)
        {
            x = X;
            y = Y;
            sym = Sym;
        }

        public Punkt(Punkt p)
        {
            x = p.x;
            y = p.y;
            sym = p.sym;
        }

        public void Move(int offset, Suunas direction)
        {
            if (direction == Suunas.RIGHT)
            {
                x = x + offset;
            }
            else if (direction == Suunas.LEFT)
            {
                x = x - offset;
            }
            else if (direction == Suunas.UP)
            {
                y = y - offset;
            }
            else
            {
                y = y + offset;
            }
        }

        public bool KasLoob(Punkt p)
        {
            return p.x == this.x && p.y == this.y;
        }

        public void Draw(int x, int y, char sym)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        public void Tuhjenda()
        {
            sym = ' ';
            Draw(x, y, sym);
        }

        public override string ToString()
        {
            return x + ", " + y + ", " + sym;
        }
    }
}
