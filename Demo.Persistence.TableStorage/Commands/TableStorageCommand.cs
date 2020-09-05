using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage.Commands
{
    public abstract class TableStorageCommand<T> : ICommand<CloudTable, T>
    {
        public abstract Task ExecuteAsync(CloudTable context);
    }
}