using System.ComponentModel.DataAnnotations;

namespace Naidis_App
{
    public class Riik
    {
        [Key]
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Rahvastik { get; set; }
        public Image Lipp { get; set; }
    }
}