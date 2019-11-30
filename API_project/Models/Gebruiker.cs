using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class Gebruiker
    {
        public int GebruikerID { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Gebruikersnaam { get; set; }
        public ICollection<PollGebruiker> PollGebruikers { get; set; }
        public ICollection<Stem> Stemmen { get; set; }
        public ICollection<Vriend> Verzonden { get; set; }
        public ICollection<Vriend> Gekregen { get; set; }
        [NotMapped]
        public string token { get; set; }

    }
}
