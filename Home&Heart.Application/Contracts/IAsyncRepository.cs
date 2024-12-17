using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Contracts
{
    public interface IAsyncRepository<T>
    {
        Task<T> AddAsync(T Obj);
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);
        Task<List<T>> GetByForeignKeyAsync(Expression<Func<T, bool>> predicate);
    }
}
