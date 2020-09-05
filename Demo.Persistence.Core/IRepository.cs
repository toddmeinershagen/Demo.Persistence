using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Persistence.Core
{
    public interface IRepository<TContext>
    {
        Task<IEnumerable<T>> FindAsync<T>(IQuery<TContext, T> query);
        Task<T> FindAsync<T>(IScalar<TContext, T> query);
        Task ExecuteAsync<T>(ICommand<TContext, T> command);
    }
}