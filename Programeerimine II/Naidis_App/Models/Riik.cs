using System.ComponentModel.DataAnnotations;

namespace Naidis_App
{
    public class Riik
    {
        [Key]
        public int Id { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public string Pealinn { get; set; } = string.Empty;
        public int Rahvastik { get; set; } = 0;
        public string Lipp { get; set; } = string.Empty;
        public string Keel { get; set; } = string.Empty;
    }
}