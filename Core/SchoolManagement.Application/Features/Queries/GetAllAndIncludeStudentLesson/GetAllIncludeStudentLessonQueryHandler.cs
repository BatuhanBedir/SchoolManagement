using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetAllAndIncludeStudentLesson
{
    public class GetAllIncludeStudentLessonQueryHandler : IRequestHandler<GetAllIncludeStudentLessonQueryRequest, GetAllIncludeStudentLessonQueryResponse>
    {
        IStudentReadRepository studentReadRepository;
        ILessonReadRepository lessonReadRepository;

        public GetAllIncludeStudentLessonQueryHandler(IStudentReadRepository studentReadRepository, ILessonReadRepository lessonReadRepository)
        {
            this.studentReadRepository = studentReadRepository;
            this.lessonReadRepository = lessonReadRepository;
        }

        public async Task<GetAllIncludeStudentLessonQueryResponse> Handle(GetAllIncludeStudentLessonQueryRequest request, CancellationToken cancellationToken)
        {//new List<Student> { await await Task.FromResult(studentReadRepository.GetByIdIncludeLessons(request.Id)) };
            GetAllIncludeStudentLessonQueryResponse getAllIncludeStudentLessonQueryResponse = new();
            getAllIncludeStudentLessonQueryResponse.Student = await studentReadRepository.GetByIdIncludeLessons(request.Id, false);
            getAllIncludeStudentLessonQueryResponse.Lessons = await lessonReadRepository.GetAllAsync(false);

            return getAllIncludeStudentLessonQueryResponse;
        }
    }
}
