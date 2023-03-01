using MediatR;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Queries.GetByStudentId
{
    public class GetByStudentIdQueryRequest:IRequest<Student>
    { 
        public Guid Id { get;set; }
        public GetByStudentIdQueryRequest(Guid id)  //Bunu unutma
        {
            Id = id;
        }
    }
}
