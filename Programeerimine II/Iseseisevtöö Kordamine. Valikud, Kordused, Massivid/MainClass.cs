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
            Ulesanne1.Ulesanne(new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Ulesanne2.Ulesanne("hall");
            Ulesanne3.Ulesanne();
        }
    }
}
