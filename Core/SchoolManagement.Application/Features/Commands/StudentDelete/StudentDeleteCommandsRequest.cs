using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentDelete
{
    public class StudentDeleteCommandsRequest: IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public StudentDeleteCommandsRequest(Guid id)
        {
            Id = id;
        }
    }
}
