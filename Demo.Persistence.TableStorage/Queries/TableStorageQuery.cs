using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage.Queries
{
    public abstract class TableStorageQuery<T> : IQuery<CloudTable, T>
    {
        public abstract Task<IEnumerable<T>> ExecuteAsync(CloudTable context);
    }
}