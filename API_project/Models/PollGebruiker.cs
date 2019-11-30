using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class PollGebruiker
    {
        public int PollGebruikerID { get; set; }
        public int? PollID { get; set;}
        public int? GebruikerID { get; set; }
        public Poll Poll { get; set; }
        public Gebruiker Gebruiker { get; set; }

    }
}
