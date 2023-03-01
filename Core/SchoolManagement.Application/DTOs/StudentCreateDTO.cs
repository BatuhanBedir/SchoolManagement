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
    public class StudentCreateDTO
    {
       // public Guid? StudentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string? SchoolId { get; set; }
        public IFormFile? Photo { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }

        
    }
}
