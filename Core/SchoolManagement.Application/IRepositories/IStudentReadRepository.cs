using SchoolManagement.Application.IRepositories.Generic;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.IRepositories
{
    public interface IStudentReadRepository:IReadRepository<Student>
    {
        Task<List<Student>> GetAllIncludeSchoolsAsync(bool tracking = true);
        Task<Student> GetByIdIncludeLessons(Guid id,bool tracking = true);
    }
}
