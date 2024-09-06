using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    internal class Ulesanne3
    {
        public static void Ulesanne()
        {
            Random random = new Random();

            int rows = random.Next(1, 10);
            int columns = random.Next(1, 20);

            string[,] seatings = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    seatings[i, j] = random.Next(2).ToString();
                }
            }

        }
    }
}
