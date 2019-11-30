using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class Vriend
    {
        public int vriendID { get; set; }
        public bool bevestigd { get; set; }
        public int? friendFrom { get; set; }
        public int? friendTo { get; set; }
        public Gebruiker GebruikerFrom { get; set; }
        public Gebruiker GebruikerTo { get; set; }

    }
}
