using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    internal class Funktsioonid
    {
        public static int[] Arvude_massiiv(int n, int m)
        {
            int[] arvud = new int[m - n + 1];
            for (int i = 0; i < arvud.Length; i++)
            {
                arvud[i] = n;
                n++;
            }
            return arvud;
        }

        public static void GenereeriJaPrindiRuudud()
        {
            Random random = new Random();
            int n = random.Next(-100, 101);
            int m = random.Next(-100, 101);
            int[] arvud;
            if (n < m)
            {
                arvud = Arvude_massiiv(n, m);
            }
            else
            {
                arvud = Arvude_massiiv(m, n);
            }
            Console.WriteLine("Numbrite ruudud vahemikus N ja M:");
            foreach (int arv in arvud)
            {
                Console.WriteLine(arv * arv);
            }
        }

        public static void Viis_Arvud_Massiiv()
        {
            int[] arvud = new int[5];
            Console.WriteLine("Sisestage viis numbrit:");
            for (int i = 0; i < 5; i++)
            {
                arvud[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Summa: {0}", arvud.Sum());
            Console.WriteLine("Aritmeetiline keskmine: {0}", arvud.Average());
            Console.WriteLine("Korrutis: {0}", arvud.Aggregate(1, (acc, x) => acc * x));
            // Aggregate запускает аккумулятор начиная от 1, acc = тоже одному и x = первому значению списка), каждый раз он их перемножает и получает результат.
        }

        public static void Nimed_Vanused()
        {
            string[] nimed = new string[5];
            int[] vanused = new int[5];
            Console.WriteLine("Sisestage nimed ja vanused:");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("Nimi: ");
                nimed[i] = Console.ReadLine();
                Console.Write("Vanus: ");
                vanused[i] = int.Parse(Console.ReadLine());
            }
            int vanimVanus = vanused.Max();
            int noorimVanus = vanused.Min();
            Console.WriteLine("Koguvanus: {0}", vanused.Sum());
            Console.WriteLine("Aritmeetiline keskmine vanus: {0}", vanused.Average());
            Console.WriteLine("Vanim inimene: {0}, Vanus: {1}", nimed[Array.IndexOf(vanused, vanimVanus)], vanimVanus);
            Console.WriteLine("Noorim inimene: {0}, Vanus: {1}", nimed[Array.IndexOf(vanused, noorimVanus)], noorimVanus);
        }

        public static void OstaElevantAra()
        {
            string kasutajaSisend;
            do
            {
                Console.WriteLine("Osta elevant!");
                kasutajaSisend = Console.ReadLine();
            } while (!(kasutajaSisend.ToLower() == "elevant" || kasutajaSisend.ToLower() == "jah"));
            // Lisasin "jah", nii et see tundus mulle loogilisem.
            Console.WriteLine("Sa ostsid elevandi.");
        }

        public static void ArvaArvutiArv()
        {
            Random random = new Random();
            int arvutiArv = random.Next(1, 21);
            int katsed = 0;
            bool arvas = false;
            Console.WriteLine("Arva number (vahemikus 1 ja 20):");
            while (katsed < 5 && !arvas)
            {
                int kasutajaArv = int.Parse(Console.ReadLine());
                katsed++;
                if (kasutajaArv == arvutiArv)
                {
                    arvas = true;
                    Console.WriteLine("Sa arvasid numbri ära.");
                }
                else if (katsed < 5)
                {
                    Console.WriteLine("Proovi uuesti.");
                }
            }
            if (!arvas)
            {
                Console.WriteLine("Oled kasutanud kõik katsed. Number oli {0}.", arvutiArv);
            }
        }

        public static void SuurimArv()
        {
            int[] neliArvu = new int[4];
            for (int i = 0; i < neliArvu.Length; i++)
            {
                Console.WriteLine("Sisesta arv {0}:", i + 1);
                neliArvu[i] = int.Parse(Console.ReadLine());
            }
            Array.Sort(neliArvu);
            Array.Reverse(neliArvu);
            Console.WriteLine("Suurim võimalik neliarvuline arv: {0}", neliArvu[0]);
        }
    }
}