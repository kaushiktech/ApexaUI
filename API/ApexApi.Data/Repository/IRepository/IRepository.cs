using System.Linq.Expressions;

namespace ApexApi.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includedProps = null);
        T Get(Expression<Func<T, bool>> filter, string? includedProps = null);

        void Add(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
