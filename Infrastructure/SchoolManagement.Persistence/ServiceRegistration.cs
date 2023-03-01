using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain.Identity;
using SchoolManagement.Persistence.Contexts;
using SchoolManagement.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence
{
    public static class ServiceRegistration
    {
        //programın içindeki hali hazırda bulunan yapıların uzantısı olacak yapılar demek-->extension method
        public static void AddPersistenceServices(this IServiceCollection services)//servicelere extension method yazma derdindeyim
        {
            services.AddDbContext<SchoolManagementDbContext>(options => options.UseSqlServer(ConnectionStringsHelper.ConnectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<SchoolManagementDbContext>();

            services.AddScoped<IStudentReadRepository, StudentReadRepository>();
            services.AddScoped<IStudentWriteRepository, StudentWriteRepository>();
            services.AddScoped<ISchoolReadRepository, SchoolReadRepository>();
            services.AddScoped<ISchoolWriteRepository, SchoolWriteRepository>();
            services.AddScoped<ILessonWriteRepository, LessonWriteRepository>();
            services.AddScoped<ILessonReadRepository, LessonReadRepository>();
        }
    }
}
