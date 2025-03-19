using System.ComponentModel.DataAnnotations;

namespace Naidis_App
{
    public class Kontakt
    {
        [Key]
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string EmailVoiTelefon { get; set; }
    }
}