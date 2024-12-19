using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino
{
    public static class Globals
    {
        public static string kasutajaTuup = "Vaataja";
        public static VaatamineVorm vaatamineVorm = new VaatamineVorm();
        public static SisselogimineVorm sisselogimineVorm = new SisselogimineVorm();
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(Globals.vaatamineVorm);
        }
    }
}