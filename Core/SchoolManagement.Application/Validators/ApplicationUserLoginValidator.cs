using FluentValidation;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Validators
{
    public class ApplicationUserLoginValidator : AbstractValidator<ApplicationUserLoginDto>
    {
        public ApplicationUserLoginValidator()
        {
            RuleFor(a => a.Email).NotEmpty().WithMessage("Gir");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Gir");
        }
    }
}
