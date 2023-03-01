using Microsoft.EntityFrameworkCore;
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
    public class StudentReadRepository : ReadRepository<Student>, IStudentReadRepository
    {
        private readonly SchoolManagementDbContext context;

        public StudentReadRepository(SchoolManagementDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Student>> GetAllIncludeSchoolsAsync(bool tracking = true)
        {
            var query = Table.Include(s => s.School).AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<Student> GetByIdIncludeLessons(Guid id,bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.Include(st => st.Lessons).FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
