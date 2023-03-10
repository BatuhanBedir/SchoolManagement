using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    public record SchoolUpdateDto:SchoolCreateDto
    {
     
        public Guid Id { get; set; }

        public string? PhotoPath { get; set; } 
    }
}
