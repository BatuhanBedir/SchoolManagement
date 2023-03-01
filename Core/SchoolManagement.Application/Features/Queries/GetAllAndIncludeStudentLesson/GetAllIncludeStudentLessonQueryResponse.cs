using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetAllAndIncludeStudentLesson
{
    public class GetAllIncludeStudentLessonQueryResponse
    {
        public List<Lesson> Lessons { get; set; }
        public Student Student { get; set; }
    }
}
