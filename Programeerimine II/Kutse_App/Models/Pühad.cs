using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    public class Pühad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Puhkuse_nimi { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Kuupaev { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}