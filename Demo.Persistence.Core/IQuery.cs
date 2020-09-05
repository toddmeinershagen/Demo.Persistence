using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Persistence.Core
{
    public interface IQuery<TContext, T>
    {
        Task<IEnumerable<T>> ExecuteAsync(TContext context);
    }
}