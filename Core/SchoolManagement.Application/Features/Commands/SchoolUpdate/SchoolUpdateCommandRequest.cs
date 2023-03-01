using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.SchoolUpdate
{
    public class SchoolUpdateCommandRequest:IRequest<CommandResponse>
    {
        public SchoolUpdateDto SchoolUpdateDto { get; set; }
        public HttpRequest HttpRequest { get; set; }
        public SchoolUpdateCommandRequest(SchoolUpdateDto schoolUpdateDto, HttpRequest httpRequest)
        {
            SchoolUpdateDto = schoolUpdateDto;
            HttpRequest = httpRequest;
        }
    }
}
