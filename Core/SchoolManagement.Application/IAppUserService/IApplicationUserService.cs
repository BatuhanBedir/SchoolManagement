using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationUserService
{
    public interface IApplicationUserHandler
    {
        public Task<string?> LoginAsync(ApplicationUserLoginDto applicationUserLoginDto);
        //string GenerateJwtToken(string email, string role);

        //UserManager<ApplicationUser> UserManager { get; }
        //SignInManager<ApplicationUser> SignInManager { get; }
    }
}
