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
        Random random = new Random();
        public static int[] Arvude_massiiv(int n, int m)
        {
            int arv;
            int[] arvud = new int[m-n];
            for (int i = 0; i < arvud.Length; i++)
            {
                arv = n;
                arvud[i] = arv;
                n++;
            }
            return arvud;
        }
        public static void viis_Arvud_Massiiv()
        {
            int[] arvud = new int[];
            for (int i = 0; i < 5; i++)
            {
                int arv = int.Parse(Console.ReadLine());
                int[i]=arv
            }
        }
    }
}
