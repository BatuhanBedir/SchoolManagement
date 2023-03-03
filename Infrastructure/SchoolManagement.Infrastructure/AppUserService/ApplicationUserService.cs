using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.ApplicationUserService;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.IToken;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.ApplicationUserService
{
    public class ApplicationUserHandler : IApplicationUserHandler
    {


        /*
            IServiceProvider arayüzü, .NET Core ve ASP.NET Core'da IOC (Inversion of Control) Container sağlamak için kullanılır.
            IOC, bir sınıfın, diğer sınıfları yaratmak veya diğer sınıflara erişmek için doğrudan kod yazması yerine, bu işlevleri bir başka sınıfa bırakmasına olanak sağlayan bir programlama prensibidir.
            IServiceProvider arayüzü, .NET Core ve ASP.NET Core'da bu prensibi uygulamak için kullanılır. IServiceProvider, uygulama boyunca kullanılan servisleri yönetir ve IOC Container olarak işlev görür. Bu servisler, uygulama içinde farklı yerlerde kullanılabilir.
            IServiceProvider, uygulamada herhangi bir sınıf tarafından kullanılabilir. Örneğin, bir Controller sınıfında IServiceProvider kullanarak servisleri alabiliriz ve ilgili işlemleri gerçekleştirebiliriz. Ayrıca, IServiceProvider arayüzü, ASP.NET Core middleware'lerinde ve diğer sınıflarda da kullanılabilir.
            IServiceProvider, .NET Core ve ASP.NET Core'da yerleşik bir arayüzdür ve Microsoft.Extensions.DependencyInjection ad alanı altında bulunan ServiceCollection sınıfından türetilen bir nesne tarafından yönetilir. Bu sınıf, ConfigureServices yöntemi ile uygulamaya servisler eklemek için kullanılır.
         */

        //Bir hizmet nesnesini almak için bir mekanizma tanımlar; diğer bir deyişle, diğer nesnelere özel destek sağlayan bir nesne.
        readonly IServiceProvider serviceProvider;
        readonly ITokenHandler tokenHandler;
        public ApplicationUserHandler(IServiceProvider serviceProvider, ITokenHandler tokenHandler)
        {
            this.serviceProvider = serviceProvider;
            this.tokenHandler = tokenHandler;
        }

        //GetService<T> : Containerdaki bir servisi almak için kullanılır.IServiceProvider arabiriminden türetilen bir nesne olan serviceProvider üzerinden çağrılır.


        //=> serviceProvider.GetService<UserManager<ApplicationUser>>(); elde edilen sonuç UserManager özelliine atamak için kullanılıyor
        public UserManager<ApplicationUser> UserManager => serviceProvider.GetService<UserManager<ApplicationUser>>(); //inject edilebilirdi.

        //kullanıcıların kimlik doğrulamasını yönetmek için kullanılır. Bu sınıf, kullanıcıları oturum açtırmak, oturum açan kullanıcıların kimliklerini doğrulamak, oturum açmış kullanıcıları yönetmek ve diğer kimlik doğrulama işlevlerini yerine getirmek için kullanılır.
        public SignInManager<ApplicationUser> SignInManager => serviceProvider.GetService<SignInManager<ApplicationUser>>();  //inject edilebilirdi.

        public async Task<string?> LoginAsync(ApplicationUserLoginDto applicationUserLoginDto)
        {
            ApplicationUser applicationUser = await UserManager.FindByEmailAsync(applicationUserLoginDto.Email);
            if (applicationUser == null)
            {
                return null;
            }
            //false olması hakkının sonsuz olması anlamı
            SignInResult signInResult = await SignInManager.CheckPasswordSignInAsync(applicationUser, applicationUserLoginDto.Password, false);
            if (!signInResult.Succeeded)
            {
                return null;
            }
            //Bu nokta mail,password doğru.Authentication başarılı
            //Yetkilendirip tokeni döndür.
            string token = tokenHandler.GenerateJwtToken(applicationUser.Email, applicationUser.ApplicationUserRole.ToString());
            return token;
        }

        //yanlış

        //public string GenerateJwtToken(string email, string role)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]);     
        //    var tokenDescriptor = new SecurityTokenDescriptor       
        //    {
        //        Audience = configuration["Token:Audience"],
        //        Issuer = configuration["Token:Issuer"],
        //        Subject = new ClaimsIdentity(new[] { new Claim("Role", role), new Claim("Email", email) }), 
        //        Expires = DateTime.UtcNow.AddMinutes(50), 
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);  
        //    return tokenHandler.WriteToken(token);  
        //}
    }
}
