using MediatR;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetAllAndIncludeStudentLesson
{
    public class GetAllIncludeStudentLessonQueryRequest:IRequest<GetAllIncludeStudentLessonQueryResponse>
    {
        public GetAllIncludeStudentLessonQueryRequest(Guid ıd)
        {
            Id = ıd;
        }

        public Guid Id { get; set; }

    }
}
