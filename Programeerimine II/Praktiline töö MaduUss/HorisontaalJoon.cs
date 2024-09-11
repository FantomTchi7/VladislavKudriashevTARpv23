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
