using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage.Queries
{
    public abstract class TableStorageScalar<T> : IScalar<CloudTable, T>
    {
        public abstract Task<T> ExecuteAsync(CloudTable context);
    }
}