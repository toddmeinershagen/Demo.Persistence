using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableStorage.Abstractions.TableEntityConverters;

namespace Demo.Persistence.TableStorage.Queries
{
    public class AllReminderTypes : TableStorageQuery<ReminderType>
    {
        public override async Task<IEnumerable<ReminderType>> ExecuteAsync(CloudTable context)
        {
            var query = context.CreateQuery<DynamicTableEntity>();
            var entities = query.Execute();
            return await Task.FromResult(entities.Select(x => x.FromTableEntity<ReminderType, int, Guid>(e => e.ClientId, e => e.Id)));
        }
    }
}