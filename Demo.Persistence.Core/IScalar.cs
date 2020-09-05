using System.Threading.Tasks;

namespace Demo.Persistence.Core
{
    public interface IScalar<TContext, T>
    {
        Task<T> ExecuteAsync(TContext context);
    }
}