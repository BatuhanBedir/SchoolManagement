using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IServices;
using SchoolManagement.Application.IServicesSchool;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.SchoolUpdate
{
    public class SchoolUpdateCommandHandler : IRequestHandler<SchoolUpdateCommandRequest, CommandResponse>
    {
        ISchoolReadRepository schoolReadRepository;
        ISchoolWriteRepository schoolWriteRepository;
        readonly IFileServiceSchool fileServiceSchool;

        public SchoolUpdateCommandHandler(ISchoolReadRepository schoolReadRepository, ISchoolWriteRepository schoolWriteRepository, IFileServiceSchool fileServiceSchool)
        {
            this.schoolReadRepository = schoolReadRepository;
            this.schoolWriteRepository = schoolWriteRepository;
            this.fileServiceSchool = fileServiceSchool;
        }

        public async Task<CommandResponse> Handle(SchoolUpdateCommandRequest request, CancellationToken cancellationToken)
        {
                CommandResponse commandResponse = new();
            School school = await schoolReadRepository.GetByIdAsync(request.SchoolUpdateDto.Id);
            if (school == null)
            {
                commandResponse.Found = false;
                return commandResponse;
            }
            school.Name = request.SchoolUpdateDto.Name;

            //fileServiceSchool.DeleteSchoolOldPhoto(school, request.SchoolUpdateDto);
            await fileServiceSchool.SaveSchoolPhotoToRootAsync(request.SchoolUpdateDto,school,request.HttpRequest);

            int dbCheck = await schoolWriteRepository.SaveAsync();
            if (dbCheck < 0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
