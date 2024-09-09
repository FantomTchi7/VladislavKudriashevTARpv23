using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class HorisontaalJoon : Kujund
    {
        public HorisontaalJoon(int xLeft, int xRight, int y, char sym)
        {
            pList = new List<Punkt>();
            for (int x = xLeft; x <= xRight; x++)
            {
                Punkt p = new Punkt(x, y, sym);
                pList.Add(p);
            }
        }
    }
}
