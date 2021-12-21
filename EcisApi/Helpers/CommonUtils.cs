using EcisApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public class CommonUtils
    {
        public const string StringChars = "0123456789abcdef";

        public static string GenerateSHA1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string GenerateJwtToken(Account account, string secret)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateV1JwtToken(Account account, string clientSecret, string clientId, string secret)
        {
            // generate token that is valid for 30 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("Id", account.Id.ToString()),
                    new Claim("Type", "ThirdParty"),
                    new Claim("ClientSecret", clientSecret),
                    new Claim("ClientId", clientId),
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetFileExtension(string fileName)
        {
            return (fileName.Split(".").LastOrDefault() ?? "").ToLower();
        }

        public static string GenerateRandomHexString(int length)
        {
            Random rand = new Random();
            var charList = StringChars.ToArray();
            string hexString = "";

            for (int i = 0; i < length; i++)
            {
                int randIndex = rand.Next(0, charList.Length);
                hexString += charList[randIndex];
            }

            return hexString;
        }
    }
}
