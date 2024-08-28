using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Random random = new Random();

            int n = random.Next(-100, 101);
            int m = random.Next(-100, 101);
            int[] arvud;
            if (n < m)
            {
                arvud = Funktsioonid.Arvude_massiiv(n, m);
            }
            else
            {
                arvud = Funktsioonid.Arvude_massiiv(m, n);
            }
            foreach (int arv in arvud)
            {
                Console.WriteLine(arv*arv);
            }
        }
    }
}
