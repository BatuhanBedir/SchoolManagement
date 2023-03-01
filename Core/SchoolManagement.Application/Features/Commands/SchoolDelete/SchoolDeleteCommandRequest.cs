using MediatR;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.SchoolDelete
{
    public class SchoolDeleteCommandRequest:IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public SchoolDeleteCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}
