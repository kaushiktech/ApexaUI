using System.Linq.Expressions;
using ApexApi.Data.Data;
using ApexApi.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApexApi.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _dbContext;
        public DbSet<T> dbSet { get; set; }
        public Repository (ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
            this.dbSet=_dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> results = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(','))
                {
                    results=results.Include(prop);
                }
            }
            return results.ToList();
        }

        public T Get(Expression<Func<T,bool>> filter, string? includeProps = null)
        {
            IQueryable<T> result = dbSet.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(','))
                {
                    result = result.Include(prop);
                }
            }
            return result.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        
    }
}
