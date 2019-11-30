using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Naam { get; set; }
        public ICollection<Optie> Opties { get; set; }
        public ICollection<PollGebruiker> Deelnemers { get; set; }

    }
}
