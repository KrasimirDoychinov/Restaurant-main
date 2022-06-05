using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);

        IQueryable<T> GetAll();

        IQueryable<T> GetAllNotDeleted();

        IEnumerable<TViewModel> GetAllMapped<TViewModel>();

        IEnumerable<TViewModel> GetAllMappedNotDeleted<TViewModel>();

        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        Task EditAsync(T entity);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<bool> RemoveAsync(string id);

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
