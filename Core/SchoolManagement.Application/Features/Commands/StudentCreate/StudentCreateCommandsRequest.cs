using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.StudentCreate
{
    public class StudentCreateCommandsRequest: IRequest<CommandResponse>//geriye döndürdüğü.
    {
        public StudentCreateDTO StudentCreateDTO { get; set; }
        public HttpRequest HttpRequest { get; set; }
        public StudentCreateCommandsRequest(StudentCreateDTO studentCreateDTO, HttpRequest httpRequest)
        {
            StudentCreateDTO = studentCreateDTO;
            HttpRequest = httpRequest;
        }
    }
}
