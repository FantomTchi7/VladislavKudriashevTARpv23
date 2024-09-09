using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class VertikaalJoon : Kujund
    {
        public VertikaalJoon(int yTop, int yBottom, int x, char sym)
        {
            pList = new List<Punkt>();
            for (int y = yTop; y <= yBottom; y++)
            {
                Punkt p = new Punkt(x, y, sym);
                pList.Add(p);
            }
        }
    }
}
