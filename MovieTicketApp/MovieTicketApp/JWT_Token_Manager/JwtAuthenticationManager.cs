using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieTicketApp.Database;
using MovieTicketApp.Models;
using MovieTicketApp.Services;
using MovieTicketApp.Interface;
namespace MovieTicketApp.JWT_Token_Manager
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IMongoCollection<AdminModel> admin;
        private readonly string key;

        public JwtAuthenticationManager(IMovieDatabaseSettings settings,string key)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            admin = database.GetCollection<AdminModel>(settings.UserCollectionName);
            this.key = key;
        }



        public string Authenticate(string UserName, string PassWord)
        {
            var results=admin.Find<AdminModel>(std => std.UserName == UserName && std.PassWord == PassWord).FirstOrDefault();
      
            if (results==null) {

                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {

                    new Claim(ClaimTypes.Name,UserName)
                }),
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

