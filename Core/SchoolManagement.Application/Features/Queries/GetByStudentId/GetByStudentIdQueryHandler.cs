using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetByStudentId
{
    public class GetByStudentIdQueryHandler : IRequestHandler<GetByStudentIdQueryRequest, Student>
    {
        readonly IStudentReadRepository studentReadRepository;

        public GetByStudentIdQueryHandler(IStudentReadRepository studentReadRepository)
        {
            this.studentReadRepository = studentReadRepository;
        }

        public async Task<Student> Handle(GetByStudentIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await studentReadRepository.GetByIdAsync(request.Id);
        }
    }
}
