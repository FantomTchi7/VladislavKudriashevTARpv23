namespace Praktiline_töö_MaduUss
{
    internal class Madu : Kujund
    {
        public Suunas direction;
        private int maxX;
        private int maxY;

        public Madu(Punkt tail, int length, Suunas Direction, int maxX, int maxY)
        {
            direction = Direction;
            this.maxX = maxX;
            this.maxY = maxY;
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

            if (head.x >= maxX - 2) head.x = 1;
            if (head.x <= 0) head.x = maxX - 3;
            if (head.y >= maxY - 1) head.y = 1;
            if (head.y <= 0) head.y = maxY - 2;

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
