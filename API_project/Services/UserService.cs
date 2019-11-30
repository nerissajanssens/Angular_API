using API_project.Helpers;
using API_project.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_project.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly PollContext _pollContext;

        public UserService(IOptions<AppSettings> appSettings, PollContext pollContext)
        {
            _appSettings = appSettings.Value;
            _pollContext = pollContext;
        }
        public Gebruiker Authenticate(
        string gebruikersnaam, string wachtwoord)
        {
            var user = _pollContext.Gebruikers.SingleOrDefault(x => x.Gebruikersnaam == gebruikersnaam && x.Wachtwoord == wachtwoord);
            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so generate jwttoken
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            { new Claim("GebruikerID", user.GebruikerID.ToString()),
              new Claim("Email" ,user.Email),
              new Claim("Gebruikersnaam",user.Gebruikersnaam)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);
            // remove password before returning
            user.Wachtwoord = null;
            return user;
        }
    }
}
