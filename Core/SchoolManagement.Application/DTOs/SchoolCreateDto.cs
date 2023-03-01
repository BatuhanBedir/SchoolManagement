using Microsoft.AspNetCore.Http;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    //public class SchoolCreateDto:LessonCreateDto olmuyor.Only records may inherit from records.(recordlar yalnızca recordlardan inherite alır.)
    public record SchoolCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
