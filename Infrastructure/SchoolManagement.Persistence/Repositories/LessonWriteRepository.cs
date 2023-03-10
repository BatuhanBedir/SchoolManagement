using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using SchoolManagement.Persistence.Contexts;
using SchoolManagement.Persistence.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence.Repositories
{
    public class LessonWriteRepository : WriteRepository<Lesson>, ILessonWriteRepository
    {
        private readonly SchoolManagementDbContext context;

        public LessonWriteRepository(SchoolManagementDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
