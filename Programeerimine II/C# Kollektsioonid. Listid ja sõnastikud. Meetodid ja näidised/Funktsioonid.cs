using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    internal class Funktsioonid
    {
        public static void Ulesanne1()
        {
            List<int> arvud = new List<int>();
            Console.WriteLine("Sisesta numbrid:");
            string arvudSisestatud = Console.ReadLine();
            arvud = arvudSisestatud.Split(' ').ToList().ConvertAll(int.Parse);

            int arv;
            int[] arvudVastus = new int[arvud.Count()];

            if (arvud.Count() == 1)
            {
                Console.WriteLine(arvud[0]);
                return;
            }
            else
            { // В классе слишком долго думал над этим, надо изначально было массивы использовать :(
                for (int i = 0; i < arvud.Count(); i++)
                {
                    if (i == 0)
                    {
                        arv = arvud.Last() + arvud[i + 1];
                    }
                    else if (i == arvud.Count() - 1)
                    {
                        arv = arvud[i - 1] + arvud[0];
                    }
                    else
                    {
                        arv = arvud[i - 1] + arvud[i + 1];
                    }
                    arvudVastus[i] = arv;
                }
            }
            for (int i = 0; i < arvudVastus.Count(); i++)
            {
                if (i != arvudVastus.Count() - 1)
                {
                    Console.Write($"{arvudVastus[i]} ");
                }
                else
                {
                    Console.WriteLine(arvudVastus[i]);
                }
            }
        }
        public static void Ulesanne2()
        {
            Random random = new Random();
            List<int> arvud = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                arvud.Add(random.Next(0, 101));
            }
            Console.WriteLine(
                string.Join(", ",
                    arvud.Where(n => n % 2 == 0) // Paaris
                         .Concat(
                    arvud.Where(n => n % 2 != 0)) // Paaritu
                )
            );
        }
        public static void Ulesanne3()
        {
            Dictionary<string, double> toodedJaKalorid = new Dictionary<string, double>();
            string nimi;
            double kalorid;

            Console.WriteLine("Sisestage toodete arv:");
            int toodearv = int.Parse(Console.ReadLine());

            for (int i = 0; i < toodearv; i++)
            {
                Console.WriteLine($"Sisestage {i+1} toote nimi:");
                nimi = Console.ReadLine();
                Console.WriteLine($"Sisestage {i+1} toote kalorid:");
                toodedJaKalorid.Add(nimi, double.Parse(Console.ReadLine()));
            }
            foreach (var key in toodedJaKalorid.Keys)
            {
                System.Console.WriteLine("{0}: {1}", key, String.Join(", ", toodedJaKalorid[key]));
            }
        }
    }
}
