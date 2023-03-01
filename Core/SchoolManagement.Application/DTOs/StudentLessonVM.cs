using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    public record StudentLessonVM
    {
        
        public Guid Id { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; } 
        public Student Student{ get; set; } 
    }
}
