using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    class Toode
    {
        public string Nimetus { get; set; }
        public int Kalorid { get; set; }
        public Toode() { }
        public Toode(string nimetus, int kalorid)
        {
            Nimetus = nimetus;
            Kalorid = kalorid;
        }
    }
}
