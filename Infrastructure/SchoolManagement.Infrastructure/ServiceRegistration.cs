using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.ApplicationUserService;
using SchoolManagement.Application.Enums;
using SchoolManagement.Application.IServices;
using SchoolManagement.Application.IServicesSchool;
using SchoolManagement.Application.IToken;
using SchoolManagement.Infrastructure.ApplicationUserService;
using SchoolManagement.Infrastructure.Services.FileServices;
using SchoolManagement.Infrastructure.Services.Token;
using SchoolManagement.Infrastructure.ServiesSchool.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileServiceSchool, FileServiceSchool>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IApplicationUserHandler, ApplicationUserHandler>();
        }
        //TODO:Factory pattern'e benzer bir yapı kurguladık.!!Factory pattern'i araştır
        public static void AddFileService(this IServiceCollection services, FileServiceType fileServiceType)
        {
            switch (fileServiceType)
            {
                case FileServiceType.Local:
                    services.AddScoped<IBaseFileService, LocalFileService>();
                    services.AddScoped<IBaseFileServicesSchool, LocalFileServiceSchool>();
                    break;
                case FileServiceType.AWS:
                    //varsa AddScoped
                    break;
                case FileServiceType.Azure:
                    //varsa AddScoped
                    break;
                case FileServiceType.Google:
                    //varsa AddScoped
                    break;
                default:
                    services.AddScoped<IBaseFileService, LocalFileService>();
                    services.AddScoped<IBaseFileServicesSchool, LocalFileServiceSchool >();
                    break;
            }
        }
    }
}
