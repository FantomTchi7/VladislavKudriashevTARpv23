using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_MaduUss
{
    class Seinad
    {
        List<Kujund> wallList;

        public Seinad(int mapWidth, int mapHeight)
        {
            wallList = new List<Kujund>();

            VertikaalJoon leftLine = new VertikaalJoon(0, mapHeight - 1, 0, '#');
            VertikaalJoon rightLine = new VertikaalJoon(0, mapHeight - 1, mapWidth - 2, '#');
            HorisontaalJoon topLine = new HorisontaalJoon(0, mapWidth - 2, 0, '#');
            HorisontaalJoon bottomLine = new HorisontaalJoon(0, mapWidth - 2, mapHeight - 1, '#');
            
            wallList.Add(leftLine);
            wallList.Add(rightLine);
            wallList.Add(topLine);
            wallList.Add(bottomLine);
        }

        internal bool KasLoob(Kujund figure)
        {
            foreach (var wall in wallList)
            {
                if (wall.KasLoob(figure))
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            foreach (var wall in wallList)
            {
                wall.Draw();
            }
        }
    }
}
