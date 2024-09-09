using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class Kujund
    {
        protected List<Punkt> pList;

        public void Draw()
        {
            foreach (Punkt p in pList)
            {
                p.Draw(p.x, p.y, p.sym);
            }
        }
    }
}
