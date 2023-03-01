using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IRepositories.Generic;
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
    internal class SchoolReadRepository : ReadRepository<School>, ISchoolReadRepository
    {
        private readonly SchoolManagementDbContext context;
        public SchoolReadRepository(SchoolManagementDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
