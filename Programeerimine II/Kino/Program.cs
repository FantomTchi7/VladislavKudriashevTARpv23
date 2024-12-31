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
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(Globals.vaatamineVorm);
        }
    }
}