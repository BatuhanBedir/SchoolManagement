using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IServices;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentCreate
{
    public class StudentCreateCommandsHandler : IRequestHandler<StudentCreateCommandsRequest, CommandResponse>
    {
        IStudentWriteRepository studentWriteRepository;
        readonly IFileService fileService;

        public StudentCreateCommandsHandler(IStudentWriteRepository studentWriteRepository, IFileService fileService)
        {
            this.studentWriteRepository = studentWriteRepository;
            this.fileService = fileService;
        }
        public async Task<CommandResponse> Handle(StudentCreateCommandsRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Student student = new();
            student.FirstName = request.StudentCreateDTO.FirstName;
            student.LastName = request.StudentCreateDTO.LastName;
            if (!string.IsNullOrWhiteSpace(request.StudentCreateDTO.SchoolId))
            {
                student.SchoolId = Guid.Parse(request.StudentCreateDTO.SchoolId);
            }
            await fileService.SaveStudentPhotoToRootAsync(request.StudentCreateDTO, student, request.HttpRequest);
            bool check = await studentWriteRepository.AddAsync(student);
            if (!check)
            {
                commandResponse.Check = check;
                return commandResponse;
            }
            int dbCheck = await studentWriteRepository.SaveAsync();
            if (dbCheck<0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
