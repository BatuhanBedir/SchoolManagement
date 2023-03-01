using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetAllSchools
{
    public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQueryRequest, List<School>>
    {
        readonly ISchoolReadRepository schoolReadRepository;
        public GetAllSchoolsQueryHandler(ISchoolReadRepository schoolReadRepository)
        {
            this.schoolReadRepository = schoolReadRepository;
        }

        public async Task<List<School>> Handle(GetAllSchoolsQueryRequest request, CancellationToken cancellationToken)
        {
            return await schoolReadRepository.GetAllAsync(false);
        }
    }
}
