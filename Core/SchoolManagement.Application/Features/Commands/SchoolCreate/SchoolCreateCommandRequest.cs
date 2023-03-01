using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.SchoolCreate
{
    public class SchoolCreateCommandRequest:IRequest<CommandResponse>
    {
        public SchoolCreateDto SchoolCreateDto { get; set; }
        public HttpRequest HttpRequest { get; set; }
        public SchoolCreateCommandRequest(SchoolCreateDto schoolCreateDto, HttpRequest httpRequest)
        {
            SchoolCreateDto = schoolCreateDto;
            HttpRequest = httpRequest;
        }
    }
}
