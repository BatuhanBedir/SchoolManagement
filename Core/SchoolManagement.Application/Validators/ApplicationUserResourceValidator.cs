using FluentValidation;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Validators
{
    public class ApplicationUserResourceValidator : AbstractValidator<ApplicationUserCreateDto>
    {
        public ApplicationUserResourceValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Required!");
        }
    }
}
