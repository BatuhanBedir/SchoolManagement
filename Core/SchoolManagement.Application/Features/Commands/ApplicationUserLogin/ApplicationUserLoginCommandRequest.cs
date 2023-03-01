using MediatR;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.ApplicationUserLogin
{
    public class ApplicationUserLoginCommandRequest : IRequest<string?>
    {
        public ApplicationUserLoginDto ApplicationUserLoginDto { get; set; }
        public ApplicationUserLoginCommandRequest(ApplicationUserLoginDto applicationUserLoginDto)
        {
            ApplicationUserLoginDto = applicationUserLoginDto;
        }
    }
}
