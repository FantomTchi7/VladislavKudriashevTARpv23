using System.Data;
using Microsoft.Data.SqlClient;

namespace Kino
{
    public class ÜhendusHaldur : IDisposable
    {
        private static readonly string ühendusString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\\KinoAndmebaas.mdf;Integrated Security=True";
        private SqlConnection ühendus;
        private bool onHävitatud = false;
        public ÜhendusHaldur()
        {
            ühendus = new SqlConnection(ühendusString);
        }
        public SqlConnection SaaÜhendus()
        {
            if (ühendus.State != ConnectionState.Open)
            {
                ühendus.ConnectionString = ühendusString;
                try { ühendus.Open(); } catch { }
            }
            return ühendus;
        }
        public void SulgeÜhendus()
        {
            if (ühendus.State == ConnectionState.Open)
            {
                ühendus.ConnectionString = ühendusString;
                ühendus.Close();
            }
        }
        protected virtual void Dispose(bool hävitamine)
        {
            if (!onHävitatud)
            {
                if (hävitamine)
                {
                    SulgeÜhendus();
                    ühendus.ConnectionString = ühendusString;
                    ühendus.Dispose();
                }
                onHävitatud = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public static class Globaalsed
    {
        private static readonly ÜhendusHaldur ühendusHaldur = new ÜhendusHaldur();
        public static SqlConnection SaaÜhendus()
        {
            return ühendusHaldur.SaaÜhendus();
        }
        public static void SulgeÜhendus()
        {
            ühendusHaldur.SulgeÜhendus();
        }
        public static string kasutajaTüüp = "Vaataja";
        public static string kasutajaNimi = "Vaataja";
        public static string kasutajaEmail = "";
        public static int kasutajaID = 3;
        public static VaatamineVorm vaatamineVorm = new VaatamineVorm();
        public static SisselogimineVorm sisselogimineVorm = new SisselogimineVorm();
        public static LooKontoVorm looKontoVorm = new LooKontoVorm();
        public static BroneerimineVorm broneerimineVorm;
    }
}