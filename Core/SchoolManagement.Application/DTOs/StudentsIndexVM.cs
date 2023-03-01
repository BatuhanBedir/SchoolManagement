using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    public record StudentsIndexVM
    {
        
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        
        public string LoggedInUserRole { get; set; }
        public string LoggedInUserEmail { get; set; }
    }
}
