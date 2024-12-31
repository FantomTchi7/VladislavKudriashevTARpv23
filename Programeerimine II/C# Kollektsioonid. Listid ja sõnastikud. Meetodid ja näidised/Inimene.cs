using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    class Inimene
    {
        public string Sugu { get; set; }
        public int Vanus { get; set; }
        public int Pikkus { get; set; }
        public int Kaal { get; set; }
        public string Eluviis { get; set; }

        public Inimene(string sugu, int vanus, int pikkus, int kaal, string eluviis)
        {
            Sugu = sugu;
            Vanus = vanus;
            Pikkus = pikkus;
            Kaal = kaal;
            Eluviis = eluviis;
        }
        
        public double BMR()
        {
            int vastus;
            if (Sugu.ToLower() == "mees")
            {
                vastus = (int)((10 * Kaal) + (6.25 * Pikkus) - (5 * Vanus) + 5);
            }
            else if (Sugu.ToLower() == "naine")
            {
                vastus = (int)((10 * Kaal) + (6.25 * Pikkus) - (5 * Vanus) - 161);
            }
            else
            {
                throw new ArgumentException("Sugu viga");
            }
            if (Eluviis.ToLower() == "istuv")
            { return vastus * 1.2; }
            else if (Eluviis.ToLower() == "vahene")
            { return vastus * 1.375; }
            else if (Eluviis.ToLower() == "moodukas")
            { return vastus * 1.55; }
            else if (Eluviis.ToLower() == "korge")
            { return vastus * 1.725; }
            else if (Eluviis.ToLower() == "vaga korge")
            { return vastus * 1.9; }
            else { return vastus; }
        }
    }
}
