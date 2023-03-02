using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;
using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence.Contexts
{
    public class SchoolManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolManagementDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Student>().Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            builder.Entity<Student>().Property(a => a.LastName).HasMaxLength(50).IsRequired();

            builder.Entity<School>().Property(a => a.Name).HasMaxLength(50).IsRequired();

            builder.Entity<School>().HasMany(st => st.Students).WithOne(s => s.School).HasForeignKey(fk => fk.SchoolId);
            builder.Entity<School>().HasData(new School
            {
                Id = Guid.NewGuid(),
                Name = "Boğaziçi"
            });
            builder.Entity<School>().HasData(new School
            {
                Id = Guid.NewGuid(),
                Name = "ODTÜ"
            });

            builder.Entity<Lesson>().Property(a => a.Name).HasMaxLength(20).IsRequired();
            builder.Entity<Lesson>().HasMany(s => s.Schools).WithMany(l => l.Lessons);
            builder.Entity<Lesson>().HasMany(st => st.Students).WithMany(l => l.Lessons);
            builder.Entity<Lesson>().HasData(new Lesson
            {
                Id=Guid.NewGuid(),
                Name="Lesson1",
            });
            builder.Entity<Lesson>().HasData(new Lesson
            {
                Id = Guid.NewGuid(),
                Name = "Lesson2",
            });

            base.OnModelCreating(builder);
        }
        ////Her entity için yapacağım bir işi her entity de ayrı ayrı yapmak yerine tek merkezden yöneteceğim. (Interceptor)
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entityler üzerinde yapılan değşiklikleri takip ederek bu verilere ulaşabilmemizi sağlar.
            var datas = ChangeTracker.Entries<BaseEntity>();//base entityden inherite almış tüm yapılar bunu kullanacağı için gönderileni save ederken aşağıdakine göre kullan.

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreationDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.Now,
                    //Delete işinde hata vermemesi için
                    _ => DateTime.Now
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
