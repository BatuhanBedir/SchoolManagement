using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.SchoolDelete
{
    public class SchoolDeleteCommandHandler : IRequestHandler<SchoolDeleteCommandRequest, CommandResponse>
    {
        ISchoolReadRepository schoolReadRepository;
        ISchoolWriteRepository schoolWriteRepository;

        public SchoolDeleteCommandHandler(ISchoolReadRepository schoolReadRepository, ISchoolWriteRepository schoolWriteRepository)
        {
            this.schoolReadRepository = schoolReadRepository;
            this.schoolWriteRepository = schoolWriteRepository;
        }

        public async Task<CommandResponse> Handle(SchoolDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            School school = await schoolReadRepository.GetByIdAsync(request.Id);
            if (school == null)
            {
                commandResponse.Check = false;
                return commandResponse;
            }
            bool check = await schoolWriteRepository.RemoveAsync(request.Id);
            if (!check)
            {
                commandResponse.Check = check;
                return commandResponse;
            }
            int dbCheck = await schoolWriteRepository.SaveAsync();
            if (dbCheck < 0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
