using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentAddLesson
{
    public class StudentAddLessonCommandHandler : IRequestHandler<StudentAddLessonCommandRequest, CommandResponse>
    {
        IStudentWriteRepository studentWriteRepository;
        IStudentReadRepository studentReadRepository;
        ILessonReadRepository lessonReadRepository;
        public StudentAddLessonCommandHandler(IStudentWriteRepository studentWriteRepository, IStudentReadRepository studentReadRepository, ILessonReadRepository lessonReadRepository)
        {
            this.studentWriteRepository = studentWriteRepository;
            this.studentReadRepository = studentReadRepository;
            this.lessonReadRepository = lessonReadRepository;
        }

        public async Task<CommandResponse> Handle(StudentAddLessonCommandRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Student student = await studentReadRepository.GetByIdIncludeLessons(request.Id);
            if (student !=null)
            {
                commandResponse.Found = false;
            }
            if(request.Ids != null) { 
            HashSet<Lesson> lessons = new HashSet<Lesson>();
            foreach (var item in request.Ids)
            {
                var lesson = await lessonReadRepository.GetByIdAsync(item);
                lessons.Add(lesson);
            }
            student.Lessons = lessons;
            }
            int dbCheck = await studentWriteRepository.SaveAsync();
            if (dbCheck>0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
            
        }
    }
}
