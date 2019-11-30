using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class Stem { 
        public int StemID { get; set; }
        public int OptieID { get; set;}
        public int GebruikerID { get; set; }
        public Optie Optie { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}
