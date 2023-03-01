using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentAddLesson
{
    public class StudentAddLessonCommandRequest:IRequest<CommandResponse>
    {
        public StudentAddLessonCommandRequest(Guid ıd, Guid[] ıds)
        {
            Id = ıd;
            Ids = ıds;
        }

        public Guid Id { get; set; }
        public Guid[] Ids { get; set; }
        
    }
}
