using MediatR;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.LessonCreate
{
    public class LessonCreateCommandRequest:IRequest<CommandResponse>
    {
        public LessonCreateDto LessonCreateDto { get; set; }
        public LessonCreateCommandRequest(LessonCreateDto lessonCreateDto)
        {
            LessonCreateDto = lessonCreateDto;
        }
    }
}
