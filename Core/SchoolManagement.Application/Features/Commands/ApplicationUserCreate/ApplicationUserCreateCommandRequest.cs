using MediatR;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.ApplicationUserCreate
{
    public class ApplicationUserCreateCommandRequest : IRequest<ApplicationUserCreateCommandResponse>
    {
        public ApplicationUserCreateDto ApplicationUserCreateDto { get; set; }
        public ApplicationUserCreateCommandRequest(ApplicationUserCreateDto applicationUserCreateDto)
        {
            ApplicationUserCreateDto = applicationUserCreateDto;
        }
    }
}
