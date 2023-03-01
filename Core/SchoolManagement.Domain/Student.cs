using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class Student:BaseEntity
    {
        public Student()
        {
            Lessons = new HashSet<Lesson>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? SchoolId { get; set; }
        public School? School { get; set; }
        public string? PhotoPath { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

    }
}
