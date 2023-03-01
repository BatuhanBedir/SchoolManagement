using MediatR;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.LessonUpdate
{
    public class LessonUpdateCommandRequest:IRequest<CommandResponse>
    {
        public LessonUpdateDto LessonUpdateDto { get; set; }
        public LessonUpdateCommandRequest(LessonUpdateDto lessonUpdateDto)
        {
            LessonUpdateDto = lessonUpdateDto;
        }
    }
}
