using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.IRepositories.Generic
{
    public interface IReadRepository<T> : IRepository<T> where T : class
    {
        //async/ await edilmek zorundadır (await olmazsa işi bitmez Task döner). await ettiğinde işi bittiğinde list verir.bunun sonucunu beklemeyen her şey devam eder.
        Task<List<T>> GetAllAsync(bool tracking = true);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
        //Task GetById(Guid id, bool tracking = true); asenkron void, hiçbir dönüş değeri yoktur.
    }
}
