using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudriashevTARpv23
{
    internal class Ulesanne1
    {
        public static void Ulesanne(int[,] kahemootmilineMassiiv)
        {
            // https://www.reddit.com/r/csharp/comments/hwz705/comment/fz3akyu/
            static U[,] Convert<T, U>(T[,] array, Func<T, U> func)
            {
                var result = new U[array.GetLength(0), array.GetLength(1)];

                for (int i = 0; i < array.GetLength(0); i++)
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        result[i, j] = func(array[i, j]);
                    }

                return result;
            } // Funktsioon ühte tüüpi kahemõõtmeliste loendite teisendamiseks teist tüüpi kahemõõtmelisteks loenditeks.
            // Jah, see varastati Internetist, kuid ma ei saanud muul viisil massi int-st stringiks teisendada.

            string[,] kahemootmilineMassiivString = Convert<int, string>(kahemootmilineMassiiv, x => x.ToString()); // Loon teisendatud massiiv

            List<string> kahemootmilineMassiivResult = new List<string>(); // Loon tulemuste loend

            for (int i = 0; i < kahemootmilineMassiivString.Length / kahemootmilineMassiivString.Rank; i++)
            {
                kahemootmilineMassiivResult.Add($"{kahemootmilineMassiivString[i, 0]}{kahemootmilineMassiivString[i, 1]}");
            } // Lisan nimekirja

            foreach (string item in kahemootmilineMassiivResult)
            {
                Console.WriteLine(item);
            } // Printin tulemus

        }

    }
}
