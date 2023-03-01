using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IServicesSchool;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.SchoolCreate
{
    public class SchoolCreateCommandHandler : IRequestHandler<SchoolCreateCommandRequest, CommandResponse>
    {
        ISchoolWriteRepository schoolWriteRepository;
        readonly IFileServiceSchool fileServiceSchool;

        public SchoolCreateCommandHandler(ISchoolWriteRepository schoolWriteRepository, IFileServiceSchool fileServiceSchool)
        {
            this.schoolWriteRepository = schoolWriteRepository;
            this.fileServiceSchool = fileServiceSchool;
        }

        public async Task<CommandResponse> Handle(SchoolCreateCommandRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            School school = new();
            school.Name = request.SchoolCreateDto.Name;
            await fileServiceSchool.SaveSchoolPhotoToRootAsync(request.SchoolCreateDto, school, request.HttpRequest);

            bool check = await schoolWriteRepository.AddAsync(school);
            if (!check)
            {
                commandResponse.Check = check;
                return commandResponse;
            }
            int dbCheck = await schoolWriteRepository.SaveAsync();
            if (dbCheck<0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
