using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class Optie
    {
        public int OptieID { get; set; }
        public string Naam { get; set; }
        public int? PollID { get; set; }
        public int Count { get; set; }
        public Poll Poll { get; set; }
        public ICollection<Stem> Stemmen { get; set; }
    }
}
