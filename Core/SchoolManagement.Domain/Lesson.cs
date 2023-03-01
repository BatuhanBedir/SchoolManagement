using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class Lesson:BaseEntity
    {
        public Lesson()
        {
            Students = new HashSet<Student>();
            Schools = new HashSet<School>();
        }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<School> Schools { get; set; }
    }
}
