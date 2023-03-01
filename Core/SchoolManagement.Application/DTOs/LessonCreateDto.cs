using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    public record LessonCreateDto
    {
        [Required(ErrorMessage ="please enter name")]
        [MaxLength(20,ErrorMessage ="To long")]
        public string Name { get; set; }
    }
}
