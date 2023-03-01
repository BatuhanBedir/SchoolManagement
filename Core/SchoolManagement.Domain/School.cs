using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class School:BaseEntity
    {
        //public School()
        //{
        //    Students = new HashSet<Student>();
        //}
        public string Name { get; set; }
        public string? PhotoPath { get; set; }
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
    }
}
