using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.IToken;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJwtToken(string email, string role)
        {
            /*
             JwtSecurityTokenHandler sınıfından bir nesne oluşturulur. Daha sonra, JWT için kullanılacak gizli anahtar, Configuration sınıfından Token:SecurityKey ayarından alınarak Encoding.UTF8.GetBytes() yöntemi ile byte dizisine dönüştürülür.

            SecurityTokenDescriptor nesnesi oluşturulur ve token'in özellikleri belirlenir. Audience, Issuer, Subject, Expires ve SigningCredentials özellikleri ayarlanır. Bu özellikler token'in geçerlilik süresi, imzalama algoritması, yayıncı, hedef kitle ve içerik (yani kullanıcının kimlik bilgileri gibi) gibi token'in özelliklerini tanımlar.

            tokenHandler.CreateToken(tokenDescriptor) yöntemi ile JWT oluşturulur ve tokenHandler.WriteToken(token) yöntemi ile de JWT imzalanır ve bir string olarak döndürülür. Bu string daha sonra, kullanıcının kimliğini doğrulamak için gerekli bir HTTP isteği sırasında, Authorization başlığına eklenir. Bu sayede sunucu, gelen isteği JWT'nin imzasıyla doğrulayarak, kullanıcının kimliğini kontrol eder.
             */

            //50 dakikalık Jwt
            //şartları sağlayan token üretiyor.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]);       // key zaten var okuyoruz.
            var tokenDescriptor = new SecurityTokenDescriptor       //tokenimin oluşturulacağı şartlar belirleniyor.
            {
                Audience = configuration["Token:Audience"],
                Issuer = configuration["Token:Issuer"],
                Subject = new ClaimsIdentity(new[] { new Claim("Role", role), new Claim("Email", email) }), //tokenime claim koyuyorum!
                Expires = DateTime.UtcNow.AddMinutes(50), //ömrü
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),//security keyimi veriyorum
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);  //oluşturulan şartları kullanarak tokeni yaratmış oluşturuyor
            return tokenHandler.WriteToken(token);  //stringe çevirip return ediyoruz.
        }
    }
}
