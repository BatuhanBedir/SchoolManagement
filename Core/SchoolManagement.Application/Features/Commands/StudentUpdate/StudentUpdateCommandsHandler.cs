using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IServices;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentUpdate
{
    public class StudentUpdateCommandsHandler : IRequestHandler<StudentUpdateCommandsRequest,CommandResponse>
    {
        IStudentWriteRepository studentWriteRepository;
        IStudentReadRepository studentReadRepository;
        readonly IFileService fileService;
        public StudentUpdateCommandsHandler(IStudentWriteRepository studentWriteRepository, IStudentReadRepository studentReadRepository, IFileService fileService)
        {
            this.studentWriteRepository = studentWriteRepository;
            this.studentReadRepository = studentReadRepository;
            this.fileService = fileService;
        }



        public async Task<CommandResponse> Handle(StudentUpdateCommandsRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Student student = await studentReadRepository.GetByIdAsync(request.StudentUpdateDto.Id);
            if (student == null)
            {
                commandResponse.Found = false;
                return commandResponse;
            }

            student.FirstName = request.StudentUpdateDto.FirstName;
            student.LastName = request.StudentUpdateDto.LastName;
            if (!string.IsNullOrWhiteSpace(request.StudentUpdateDto.SchoolId))
            {
                student.SchoolId = Guid.Parse(request.StudentUpdateDto.SchoolId);
            }

            fileService.DeleteStudentOldPhoto(student, request.StudentUpdateDto);  
            await fileService.SaveStudentPhotoToRootAsync(request.StudentUpdateDto, student, request.HttpRequest);
            int dbCheck = await studentWriteRepository.SaveAsync();
            if (dbCheck < 1)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
