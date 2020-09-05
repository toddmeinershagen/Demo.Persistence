using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage.Queries
{
    public class AllReminderTypes : TableStorageQuery<ReminderType>
    {
        public override async Task<IEnumerable<ReminderType>> ExecuteAsync(CloudTable context)
        {
            var query = context.CreateQuery<TableEntityAdapter<ReminderType>>();
            var entities = query.Execute();
            return await Task.FromResult(entities.Select(x => x.OriginalEntity));
        }
    }
}