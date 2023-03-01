using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.LessonDelete
{
    public class LessonDeleteCommandRequest:IRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public LessonDeleteCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}
