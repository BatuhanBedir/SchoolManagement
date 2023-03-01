using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentUpdate
{
    public class StudentUpdateCommandsRequest : IRequest<CommandResponse>
    {
        public StudentUpdateDto StudentUpdateDto { get; set; }
        public HttpRequest HttpRequest { get; set; }
        public StudentUpdateCommandsRequest(StudentUpdateDto studentUpdateDto, HttpRequest httpRequest)
        {
            StudentUpdateDto = studentUpdateDto;
            HttpRequest = httpRequest;
        }
    }
}
