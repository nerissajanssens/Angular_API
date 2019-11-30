using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class DBInitializer
    {
        public static void Initialize(PollContext context)
        {
            context.Database.EnsureCreated();
            //Look for any
            if (context.Gebruikers.Any())
            {
                return;
            }

            context.Gebruikers.AddRange(
                new Gebruiker { Gebruikersnaam = "nerissa", Email = "n@n.com", Wachtwoord = "test" },
                new Gebruiker { Gebruikersnaam = "jef", Email = "j@j.com", Wachtwoord = "test"});

            context.SaveChanges();

            context.Polls.AddRange(
                new Poll { Naam = "Poll 1"},
                new Poll { Naam = "Poll 2" },
                new Poll { Naam = "Poll 3" });

            context.SaveChanges();

            context.PollGebruikers.AddRange(
                new PollGebruiker { GebruikerID = 1, PollID = 1 },
                new PollGebruiker { GebruikerID = 1, PollID = 2 },
                new PollGebruiker { GebruikerID = 2, PollID = 2 });

            context.SaveChanges();

            context.Opties.AddRange(
                new Optie { Naam = "Optie1", PollID = 1 },
                new Optie { Naam = "Optie2", PollID = 2 },
                new Optie { Naam = "Optie3", PollID = 2 },
                new Optie { Naam = "Optie4", PollID = 1 },
                new Optie { Naam = "Optie5", PollID = 1 });

            context.SaveChanges();

            context.Stemmen.AddRange(
                new Stem { OptieID = 2, GebruikerID = 2},
                new Stem { OptieID = 3, GebruikerID = 1},
                new Stem { OptieID = 5, GebruikerID = 1},
                new Stem { OptieID = 2, GebruikerID = 2}
                );


            context.Vrienden.AddRange(
            new Vriend { friendFrom = 1, friendTo = 2 });

            context.SaveChanges();
        }
    }
}
