using Praktiline_töö_MaduUss;

class Kujund
{
    public List<Punkt> pList;

    public virtual void Draw()
    {
        foreach (Punkt p in pList)
        {
            p.Draw(p.x, p.y, p.sym);
        }
    }

    internal bool KasLoob(Kujund figure)
    {
        foreach (var p in pList)
        {
            if (figure.KasLoob(p))
            {
                return true;
            }
        }
        return false;
    }

    internal bool KasLoob(Punkt point)
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
