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
using System.Security.Cryptography;

namespace MovieTicketApp.JWT_Token_Manager
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IMongoCollection<AdminModel> admin;
        private readonly string key;
        private readonly string passkey; 

        public JwtAuthenticationManager(IMovieDatabaseSettings settings,string key)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            admin = database.GetCollection<AdminModel>(settings.UserCollectionName);
            this.key = key;
            passkey = settings.PassKey;
        }



        public string Authenticate(string UserName, string PassWord)
        {
            PassWord = EncryptString(passkey, PassWord);
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


        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}

