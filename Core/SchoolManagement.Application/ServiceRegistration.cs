using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration));
            services.AddAutoMapper(typeof(ServiceRegistration));
            //services.AddFluentValidation(typeof(ServiceRegistration));
            //services.AddMvcCore()
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ServiceRegistration>());
        }
    }
}
