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

        public virtual void Draw()
        {
            foreach (Punkt p in pList)
            {
                p.Draw(p.x, p.y, p.sym);
            }
        }

        internal bool KasLoob(Kujund figure)
        {
            foreach(var p in pList)
            {
                if (figure.KasLoob(p))
                {
                    return true;
                }
            }
            return false;
        }

        private bool KasLoob(Punkt point)
        {
            foreach (var p in pList)
            {
                if (p.KasLoob(point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
