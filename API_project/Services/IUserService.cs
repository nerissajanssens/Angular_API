using API_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Services
{
    public interface IUserService
    {
        Gebruiker Authenticate(string gebruikersnaam, string wachtwoord);
    }
}
