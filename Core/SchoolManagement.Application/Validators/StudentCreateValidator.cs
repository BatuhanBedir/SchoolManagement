using FluentValidation;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Validators
{
    public class StudentCreateValidator:AbstractValidator<StudentCreateDTO>
    {
        public StudentCreateValidator()
        {
            RuleFor(a => a.FirstName).NotNull().NotEmpty().WithMessage("Gir").MinimumLength(4).WithMessage("Bu kadar kısa olamaz");
            RuleFor(a => a.LastName).NotNull().NotEmpty().WithMessage("Gir");
        }
    }
}
