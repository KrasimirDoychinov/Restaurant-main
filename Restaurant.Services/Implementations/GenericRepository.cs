using Restaurant.Data;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDeletableModel
    {
        protected readonly ApplicationDbContext context;
        private readonly string typeName = typeof(T).Name;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await this.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await this.context.Set<T>().AddRangeAsync(entities);
            await this.SaveChangesAsync();

        }

        public async Task EditAsync(T entity)
        {
            entity.ModifiedOn_17118018 = DateTime.Now;
            await this.SaveChangesAsync();

        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
            => this.context
            .Set<T>()
            .Where(expression);
        

        public IQueryable<T> GetAll() 
            => this.context
            .Set<T>()
            .OrderByDescending(x => x.ModifiedOn_17118018);

        public IEnumerable<TViewModel> GetAllMapped<TViewModel>()
            => this.context
            .Set<T>()
            .OrderByDescending(x => x.ModifiedOn_17118018)
            .To<TViewModel>()
            .ToList();

        public IEnumerable<TViewModel> GetAllMappedNotDeleted<TViewModel>()
            => this.context
            .Set<T>()
            .OrderByDescending(x => x.ModifiedOn_17118018)
            .Where(x => !x.IsDeleted)
            .To<TViewModel>()
            .ToList();

        public IQueryable<T> GetAllNotDeleted()
            => this.context
            .Set<T>()
            .OrderByDescending(x => x.ModifiedOn_17118018)
            .Where(x => !x.IsDeleted);

        public T GetById(string id)
            => this.context
            .Set<T>()
            .Find(id);


        public async Task<bool> RemoveAsync(string id)
        {
            var item = this.GetById(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            item.ModifiedOn_17118018 = DateTime.Now;
            await this.SaveChangesAsync();
            return true;
        }

        public void SaveChanges()
            => this.context.SaveChanges();

        public async Task SaveChangesAsync()
            => await this.context.SaveChangesAsync();
    }
}
