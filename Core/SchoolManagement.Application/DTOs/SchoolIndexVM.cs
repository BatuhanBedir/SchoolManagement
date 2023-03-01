using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    public record SchoolIndexVM
    {
        public List<School> Schools { get; set; } = new List<School>();
    }
}
