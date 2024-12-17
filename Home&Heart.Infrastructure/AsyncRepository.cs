using Home_Heart.Application.Contracts;
using Home_Heart.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Infrastructure
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly HomeContext db;

        public AsyncRepository(HomeContext db)
        {
            this.db = db;
        }
        public async Task<T> AddAsync(T Obj)
        {
            await db.Set<T>().AddAsync(Obj);
            await db.SaveChangesAsync();
            return Obj;
        }
        public async Task<IList<T>> GetAllAsync()
        {
            var data = await db.Set<T>().Select(a => a).ToListAsync();
            return data;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            var Obj = await db.Set<T>().FindAsync(id);
            return Obj;
        }
 
        public async Task<List<T>> GetByForeignKeyAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
