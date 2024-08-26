using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    public class Funktsioonid
    {
        public static void Tere(string nimi)
        {
            Console.WriteLine("Tere kallis {0}", nimi);
        }

        public static int Liitmine(int arv1, int arv2)
        {
            return arv1 + arv2;
        }

        // Arvuta() funktsioon, mis sõltub 3 parameetrist: tehe, arv1, arv2. Kasuta if konstruktsioon
        public static int Arvuta(string operatsioon, int arv1, int arv2)
        {
            int tempArv = 0;
            if (operatsioon == "+")
            {
                tempArv = arv1 + arv2;
            }
            else if (operatsioon == "-")
            {
                tempArv = arv1 - arv2;
            }
            else if (operatsioon == "*")
            {
                tempArv = arv1 * arv2;
            }
            else if (operatsioon == "/")
            {
                tempArv = arv1 / arv2;
            }
            else if (operatsioon == "==")
            {
                if (arv1 != arv2)
                {
                    Console.WriteLine("Viga");
                    tempArv = 0;
                }
                else
                {
                    tempArv = 1;
                }
            }
            return tempArv;
        }

        public static string korgusUtlema(string sugu, int korgus)
        {
            string tulemus = "";
            if (sugu == "mees")
            {
                if (korgus <= 160)
                {
                    tulemus = "lühike";
                }
                else if (korgus <= 190)
                {
                    tulemus = "keskmine";
                }
                else
                {
                    tulemus = "pikk";
                }
            }
            else if (sugu == "naine")
            {
                if (korgus <= 150)
                {
                    tulemus = "lühike";
                }
                else if (korgus <= 180)
                {
                    tulemus = "keskmine";
                }
                else
                {
                    tulemus = "pikk";
                }
            }
            return tulemus;
        }

        public static string temperatuurUle18(int temperatuur)
        {
            string tulemus = "0";
            if (temperatuur >= 18)
            {
                tulemus = "temperatuur kõrgem 18st";
                return tulemus;
            }
            else
            {
                tulemus = "temperatuur vähem 18st";
            }
            return tulemus;
        }

        public static void juku()
        {
            Console.WriteLine("Tere tulemast!");
            string eesnimi = Console.ReadLine();
            Console.WriteLine("Tere, " + eesnimi);
            if (eesnimi.ToLower() == "juku")
            {
                Console.WriteLine("Tule minuga kinno!");
                Console.WriteLine("Kui vana sa oled?");
                int vanus = Console.ReadLine();
                if (vanus < 0 || vanus < 100)
                {
                    Console.WriteLine("Viga andmetega");
                }
                else if (vanus < 6)
                {
                    Console.WriteLine("Tasuta");
                }
                else if (vanus < 14)
                {
                    Console.WriteLine("Lastepilet");
                }
                else if (vanus < 65)
                {
                    Console.WriteLine("Täispilet");
                }
                else if (vanus < 65)
                {
                    Console.WriteLine("Täispilet");
                }
                else if (vanus > 65)
                {
                    Console.WriteLine("Sooduspilet");
                }
            }
            else
            {
                Console.WriteLine("Täna mind kodus pole!");
            }
        }
    }
}
