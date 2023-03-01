using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetAllStudentsWithSchools
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQueryRequest, List<Student>>
    {
        readonly IStudentReadRepository studentReadRepository;

        public GetAllStudentsQueryHandler(IStudentReadRepository studentReadRepository)
        {
            this.studentReadRepository = studentReadRepository;
        }

        public async Task<List<Student>> Handle(GetAllStudentsQueryRequest request, CancellationToken cancellationToken)
        {
            return await studentReadRepository.GetAllIncludeSchoolsAsync(false);
        }
    }
}
