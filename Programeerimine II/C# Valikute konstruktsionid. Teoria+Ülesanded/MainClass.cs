using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            // I.osa Andmetüübid, Alamfunktsioonid
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Hello, World!");
            string nimetus = "Python";
            Console.WriteLine("Hello, {0}", nimetus);
            Funktsioonid.Tere("Vlad");
            Console.Write("Sisesta esimene arv: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Sisesta teine arv: ");
            int b = int.Parse(Console.ReadLine());
            int vastus = Funktsioonid.Liitmine(a, b);
            if (vastus == 0)
            {
                Console.WriteLine("Vale number");
            }
            else
            {
                Console.WriteLine("Vastus: {0}", vastus);
            }
            double arv = 3.141592653589793238462643383279502884197;
            Console.WriteLine("Vastus: {0}", Funktsioonid.Liitmine(a, (int)arv));
            char taht = 'A';
            Console.WriteLine(taht);
            Console.WriteLine(Funktsioonid.Arvuta("==", 8, 8));
            Console.WriteLine(Funktsioonid.korgusUtlema("mees", 170));
            Console.WriteLine(Funktsioonid.temperatuurUle18(17));
            Funktsioonid.juku("Juku");
            Funktsioonid.naaber();
            
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                int paev_nr = random.Next(1, 8);
                Console.WriteLine(Funktsioonid.paevad(paev_nr));
            }

            string[] nimed = new string[5] {"Anna", "Inna", "Oksana", "Pavel", "Karl"};
            for (int i = 0; i < nimed.Length; i++)
            {
                Console.WriteLine(nimed[i]);
            }
            foreach (string nimi in nimed)
            {
                Console.WriteLine(nimi);
            }
            int n = 0;
            while (n < nimed.Length)
            {
                Console.WriteLine(nimed[n]);
                n++;
            }
            do
            {
                Console.WriteLine(nimed[n-1]);
                n--;
            }
            while (n != 0);
        }
    }
}