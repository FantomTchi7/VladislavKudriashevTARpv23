using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    class Maakond
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }

        public Maakond(string nimi, string pealinn)
        {
            Nimi = nimi;
            Pealinn = pealinn;
        }
    }

    class Sonastik
    {
        private Dictionary<string, string> maakonnad = new Dictionary<string, string>();

        public Sonastik()
        {
            maakonnad.Add("Harju", "Tallinn");
            maakonnad.Add("Hiiu", "Kärdla");
            maakonnad.Add("Ida-Viru", "Jõhvi");
            maakonnad.Add("Jõgeva", "Jõgeva");
            maakonnad.Add("Järva", "Paide");
            maakonnad.Add("Lääne", "Haapsalu");
            maakonnad.Add("Lääne-Viru", "Rakvere");
            maakonnad.Add("Põlva", "Põlva");
            maakonnad.Add("Pärnu", "Pärnu");
            maakonnad.Add("Rapla", "Rapla");
            maakonnad.Add("Saare", "Kuressaare");
            maakonnad.Add("Tartu", "Tartu");
            maakonnad.Add("Valga", "Valga");
            maakonnad.Add("Viljandi", "Viljandi");
            maakonnad.Add("Võru", "Võru");
        }

        public void NaitaTeaved()
        {
            foreach (KeyValuePair<string, string> pair in maakonnad)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
        }

        public string LeiaPealinn(string maakond)
        {
            if (maakonnad.ContainsKey(maakond))
            {
                return maakonnad[maakond];
            }
            else
            {
                return null;
            }
        }

        public string LeiaMaakond(string linn)
        {
            foreach (var paar in maakonnad)
            {
                if (paar.Value.Equals(linn, StringComparison.OrdinalIgnoreCase))
                {
                    return paar.Key;
                }
            }
            return null;
        }

        public void LisaMaakond(string maakond, string pealinn)
        {
            if (!maakonnad.ContainsKey(maakond))
            {
                maakonnad.Add(maakond, pealinn);
            }
            else
            {
                Console.WriteLine("See maakond on juba olemas sõnastikus.");
            }
        }

        public void TestiTeadmisi()
        {
            int õiged = 0;
            int valed = 0;
            Random random = new Random();
            List<string> maakondadeNimed = new List<string>(maakonnad.Keys);

            for (int i = 0; i < maakondadeNimed.Count; i++)
            {
                string juhuslikMaakond = maakondadeNimed[random.Next(maakondadeNimed.Count)];
                Console.WriteLine($"Mis on maakonna {juhuslikMaakond} pealinn?");
                string kasutajaVastus = Console.ReadLine();

                if (kasutajaVastus.Equals(maakonnad[juhuslikMaakond], StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Õige!");
                    õiged++;
                }
                else
                {
                    Console.WriteLine($"Vale! Õige vastus on {maakonnad[juhuslikMaakond]}.");
                    valed++;
                }
            }

            double protsent = (double)õiged / maakondadeNimed.Count * 100;
            Console.WriteLine($"Testi tulemus: {protsent}% õigeid vastuseid.");
        }
    }
}
