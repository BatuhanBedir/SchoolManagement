using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.ApplicationUserCreate
{
    public class ApplicationUserCreateCommandHandler : IRequestHandler<ApplicationUserCreateCommandRequest, ApplicationUserCreateCommandResponse>
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly IMapper mapper;
        public ApplicationUserCreateCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ApplicationUserCreateCommandResponse> Handle(ApplicationUserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = new();
            applicationUser = mapper.Map<ApplicationUserCreateDto, ApplicationUser>(request.ApplicationUserCreateDto, applicationUser);
            IdentityResult identityResult = await userManager.CreateAsync(applicationUser,request.ApplicationUserCreateDto.Password);

            ApplicationUserCreateCommandResponse applicationUserCreateCommandResponse = new();
            if (!identityResult.Succeeded)
            {
                applicationUserCreateCommandResponse.Succeeded = false;
                foreach (var error in identityResult.Errors)
                {
                    applicationUserCreateCommandResponse.Message += $"{error.Code} - {error.Description}\n";
                }
            }
            else
            {
                applicationUserCreateCommandResponse.Succeeded = true;
                applicationUserCreateCommandResponse.Message = "User creation successfull";
            }

            return applicationUserCreateCommandResponse;
        }
    }
}
