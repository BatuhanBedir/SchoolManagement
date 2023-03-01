using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.IToken;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.ApplicationUserLogin
{
    public class ApplicationUserLoginCommandHandler : IRequestHandler<ApplicationUserLoginCommandRequest, string?>
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        readonly ITokenHandler tokenHandler;

        public ApplicationUserLoginCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenHandler tokenHandler)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenHandler = tokenHandler;
        }

        public async Task<string?> Handle(ApplicationUserLoginCommandRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = await userManager.FindByEmailAsync(request.ApplicationUserLoginDto.Email);
            if (applicationUser == null)
            {
                return null;
            }
            //false olması hakkının sonsuz olması anlamı
            SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(applicationUser, request.ApplicationUserLoginDto.Password, false);
            if (!signInResult.Succeeded)
            {
                return null;
            }
            //Bu nokta mail,password doğru.Authentication başarılı
            //Yetkilendirip tokeni döndür.
            string token = tokenHandler.GenerateJwtToken(applicationUser.Email, applicationUser.ApplicationUserRole.ToString());
            return token;
        }
    }
}
