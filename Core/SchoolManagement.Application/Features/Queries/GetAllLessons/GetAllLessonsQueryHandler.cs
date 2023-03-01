using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetAllLessons
{
    public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQueryRequest, List<Lesson>>
    {
        readonly ILessonReadRepository lessonReadRepository;

        public GetAllLessonsQueryHandler(ILessonReadRepository lessonReadRepository)
        {
            this.lessonReadRepository = lessonReadRepository;
        }

        public async Task<List<Lesson>> Handle(GetAllLessonsQueryRequest request, CancellationToken cancellationToken)
        {
            return await lessonReadRepository.GetAllAsync(false);
        }
    }
}
