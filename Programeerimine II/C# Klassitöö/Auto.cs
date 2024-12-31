using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    internal class Auto
    {
        public string RegNumber { get; set; }
        public Color Varv {  get; set; }
        public Inimene Omanik { get; set; }
        public Auto() { }
        public Auto(string regNumber, Color varv, Inimene omanik)
        {
            RegNumber = regNumber;
            Varv = varv;
            Omanik = omanik;
        }
        public void KelleOmaAuto()
        {
            Console.WriteLine($"{Varv.Name} auto regnumbriga {RegNumber} on {Omanik.Nimi} oma");
        }
    }
}
