using System;
using System.Collections;
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
            // Console.OutputEncoding = Encoding.UTF8;
            // Console.WriteLine("Hello, World!");
            // string nimetus = "Python";
            // Console.WriteLine("Hello, {0}", nimetus);
            // Funktsioonid.Tere("Vlad");
            // Console.Write("Sisesta esimene arv: ");
            // int a = int.Parse(Console.ReadLine());
            // Console.Write("Sisesta teine arv: ");
            // int b = int.Parse(Console.ReadLine());
            // int vastus = Funktsioonid.Liitmine(a, b);
            // if (vastus == 0)
            // {
            //     Console.WriteLine("Vale number");
            // }
            // else
            // {
            //     Console.WriteLine("Vastus: {0}", vastus);
            // }
            // double arv = 3.141592653589793238462643383279502884197;
            // Console.WriteLine("Vastus: {0}", Funktsioonid.Liitmine(a, (int)arv));
            // char taht = 'A';
            // Console.WriteLine(taht);
            // Console.WriteLine(Funktsioonid.Arvuta("==", 8, 8));
            // Console.WriteLine(Funktsioonid.korgusUtlema("mees", 170));
            // Console.WriteLine(Funktsioonid.temperatuurUle18(17));
            // Funktsioonid.juku("Juku");
            // Funktsioonid.naaber();
            // 
            // Random random = new Random();
            // for (int i = 0; i < 8; i++)
            // {
            //     int paev_nr = random.Next(1, 8);
            //     Console.WriteLine(Funktsioonid.paevad(paev_nr));
            // }
            // 
            // string[] nimed = new string[5] { "Anna", "Inna", "Oksana", "Pavel", "Karl" };
            // for (int i = 0; i < nimed.Length; i++)
            // {
            //     Console.WriteLine(nimed[i]);
            // }
            // foreach (string nimi in nimed)
            // {
            //     Console.WriteLine(nimi);
            // }
            // int n = 0;
            // while (n < nimed.Length)
            // {
            //     Console.WriteLine(nimed[n]);
            //     n++;
            // }
            // do
            // {
            //     Console.WriteLine(nimed[n - 1]);
            //     n--;
            // }
            // while (n != 0);

            //II. osa listid ja sõnastikud
            // List<string> abc = new List<string>();
            // try
            // {
            //     foreach (string rida in File.ReadAllLines(@"..\..\..\ABC.txt"))
            //     {
            //         abc.Add(rida);
            //     }
            // }
            // catch (Exception)
            // {
            //     Console.WriteLine("Fail ei saa leida!");
            // }
            // foreach (string e in abc)
            // {
            //     Console.WriteLine(e);
            // }
            // 
            // ArrayList arrayList = new ArrayList();
            // arrayList.Add("Esimene");
            // arrayList.Add("Teine");
            // arrayList.Add("Kolmas");
            // Console.WriteLine("Otsing: ");
            // string vas = Console.ReadLine();
            // if (vas != null && arrayList.Contains(vas))
            // {
            //     Console.WriteLine("Otsitav element asub " + arrayList.IndexOf(vas) + ". kohal");
            // }
            // else
            // {
            //     Console.WriteLine("Kokku oli " + arrayList.Count + " elemente, vaid otsitav puudub");
            // }
            // arrayList.Clear();
            // arrayList.Insert(1, "Anna");
            // arrayList.Insert(0, "Inna");
            // foreach (string e in arrayList)
            // {
            //     Console.WriteLine(e);
            // }

            //III. osa OOP
            List<Inimene> inimesed = new List<Inimene>();
            Inimene inimene1 = new Inimene();
            inimene1.Nimi = "Pjotr";
            inimene1.Vanus = 352;
            Inimene inimene2 = new Inimene("Jelizaveta");
            inimene2.Vanus = 98;
            Inimene inimene3 = new Inimene("Vlad", 17);
            inimesed.Add(inimene1);
            inimesed.Add(inimene2);
            inimesed.Add(inimene3);
            inimesed.Add(new Inimene("Irina", 18));

            foreach (Inimene inimene in inimesed)
            {
                Console.WriteLine($"{inimene.Nimi} on {inimene.Vanus} aastat vana");
            }
        }
    }
}