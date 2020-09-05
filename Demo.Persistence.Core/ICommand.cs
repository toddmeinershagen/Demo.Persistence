using System.Threading.Tasks;

namespace Demo.Persistence.Core
{
    public interface ICommand<TContext, T>
    {
        Task ExecuteAsync(TContext context);
    }
}