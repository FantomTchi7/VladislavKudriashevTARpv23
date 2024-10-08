﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            List<Toode> tooted = new List<Toode>();
            Console.WriteLine("Sisestage toodete arv:");
            int toodearv = int.Parse(Console.ReadLine());
            string nimetus;
            int kalorid;

            for (int i = 0; i < toodearv; i++)
            {
                Console.WriteLine($"Sisestage {i+1} toote nimi:");
                nimetus = Console.ReadLine();
                Console.WriteLine($"Sisestage {i+1} toote kalorid:");
                kalorid = int.Parse(Console.ReadLine());
                Toode toode = new Toode(nimetus, kalorid);
                tooted.Add(toode);
            }

            //for (int i = 0; i < tooted.Count; i++)
            //{
            //    Console.WriteLine($"Toode {i + 1}: Nimi = {tooted[i].Nimetus}, Kalorid = {tooted[i].Kalorid}");
            //}

            Console.WriteLine("Mis on sinu sugu?");
            string sugu = Console.ReadLine();
            while (!(sugu.ToLower() == "mees" || sugu.ToLower() == "naine"))
            {
                Console.WriteLine("Proovi uuesti:");
                sugu = Console.ReadLine();
            }

            Console.WriteLine("Mis on sinu vanus?");
            int vanus = int.Parse(Console.ReadLine());
            Console.WriteLine("Mis on sinu pikkus?");
            int pikkus = int.Parse(Console.ReadLine());
            Console.WriteLine("Mis on sinu kaal?");
            int kaal = int.Parse(Console.ReadLine());

            Console.WriteLine("Mis on sinu eluviis?");
            string eluviis = Console.ReadLine();
            while (!new string[] { "istuv", "vahene", "moodukas", "korge", "vaga korge" }.Contains(eluviis.ToLower()))
            {
                Console.WriteLine("Proovi uuesti:");
                eluviis = Console.ReadLine();
            }

            Inimene inimene = new Inimene(sugu, vanus, pikkus, kaal, eluviis);
            double vastus = inimene.BMR();

            foreach (Toode toode in tooted)
            {
                double kogus = vastus / toode.Kalorid * 100;
                Console.WriteLine($"{toode.Nimetus}: {kogus} grammi");
            }
        }

        public static void Ulesanne4()
        {
            Sonastik sonastik = new Sonastik();

            while (true)
            {
                Console.WriteLine("\nVali tegevus:");
                Console.WriteLine("1. Näita maakonnad ja pealinnad");
                Console.WriteLine("2. Leia pealinn maakonna järgi");
                Console.WriteLine("3. Leia maakond pealinna järgi");
                Console.WriteLine("4. Lisa uus maakond");
                Console.WriteLine("5. Testi oma teadmisi");
                Console.WriteLine("6. Välju");
                Console.Write("Valik: ");
                int valik = int.Parse(Console.ReadLine());

                switch (valik)
                {
                    case 1:
                        sonastik.NaitaTeaved();
                        break;

                    case 2:
                        Console.Write("Sisesta maakonna nimi: ");
                        string maakond = Console.ReadLine();
                        string pealinn = sonastik.LeiaPealinn(maakond);
                        if (pealinn != null)
                        {
                            Console.WriteLine($"Maakonna {maakond} pealinn on {pealinn}.");
                        }
                        else
                        {
                            Console.WriteLine("Sellist maakonda ei leitud.");
                        }
                        break;

                    case 3:
                        Console.Write("Sisesta pealinna nimi: ");
                        string linn = Console.ReadLine();
                        string maakondNimi = sonastik.LeiaMaakond(linn);
                        if (maakondNimi != null)
                        {
                            Console.WriteLine($"Pealinn {linn} asub {maakondNimi} maakonnas.");
                        }
                        else
                        {
                            Console.WriteLine("Sellist pealinna ei leitud.");
                        }
                        break;

                    case 4:
                        Console.Write("Sisesta uus maakond: ");
                        string uusMaakond = Console.ReadLine();
                        Console.Write("Sisesta selle pealinn: ");
                        string uusPealinn = Console.ReadLine();
                        sonastik.LisaMaakond(uusMaakond, uusPealinn);
                        Console.WriteLine("Maakond lisatud!");
                        break;

                    case 5:
                        sonastik.TestiTeadmisi();
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Vale valik, proovi uuesti.");
                        break;
                }
            }
        }
    }
}
