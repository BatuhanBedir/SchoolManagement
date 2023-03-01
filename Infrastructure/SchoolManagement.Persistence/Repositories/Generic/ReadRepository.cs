using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.IRepositories.Generic;
using SchoolManagement.Domain.Common;
using SchoolManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence.Repositories.Generic
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        readonly SchoolManagementDbContext context;
        public ReadRepository(SchoolManagementDbContext context)
        {
            this.context = context;
        }
        public DbSet<T> Table => context.Set<T>();  //sadece get varsa böyle yazılabilir.get olarak çalışıyor

        public async Task<List<T>> GetAllAsync(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.ToListAsync();   //ef de async olan var olmayan var. get yapısının async ye çevirme işleme.
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            var query = Table.Where(expression);    //runtimeda belirleniyor."var"
            if (!tracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }
    }
}
