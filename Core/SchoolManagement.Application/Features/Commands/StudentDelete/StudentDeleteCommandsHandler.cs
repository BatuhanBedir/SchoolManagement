using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentDelete
{
    public class StudentDeleteCommandsHandler : IRequestHandler<StudentDeleteCommandsRequest, CommandResponse>
    {
        readonly IStudentReadRepository studentReadRepository;
        readonly IStudentWriteRepository studentWriteRepository;

        public StudentDeleteCommandsHandler(IStudentReadRepository studentReadRepository, IStudentWriteRepository studentWriteRepository)
        {
            this.studentReadRepository = studentReadRepository;
            this.studentWriteRepository = studentWriteRepository;
        }

        public async Task<CommandResponse> Handle(StudentDeleteCommandsRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Student student = await studentReadRepository.GetByIdAsync(request.Id);
            if (student == null)
            {
                commandResponse.DbCheck = 0;
                return commandResponse;
            }
            studentWriteRepository.Remove(student);
            int dbCheck = await studentWriteRepository.SaveAsync();
            if (dbCheck < 0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
