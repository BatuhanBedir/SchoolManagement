using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolManagement.Application.IRepositories.Generic;
using SchoolManagement.Domain.Common;
using SchoolManagement.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence.Repositories.Generic
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        readonly SchoolManagementDbContext context;
        public WriteRepository(SchoolManagementDbContext context)
        {
            this.context = context;
        }
        public DbSet<T> Table => context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;  //sen takip ederken eklendiyse(added) true dönebilirsin
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }
        //public bool Update(T entity, Guid Id)
        //{
            
        //}

        public async Task<bool> RemoveAsync(Guid id)
        {
            T entity = await Table.FindAsync(id);
            return Remove(entity);
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

        
    }
}
