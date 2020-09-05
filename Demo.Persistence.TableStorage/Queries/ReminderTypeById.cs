using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage.Queries
{
    public class ReminderTypeById : TableStorageScalar<ReminderType>
    {
        private readonly Guid _id;

        public ReminderTypeById(Guid id)
        {
            _id = id;
        }

        public override async Task<ReminderType> ExecuteAsync(CloudTable context)
        {
            var query = context.CreateQuery<TableEntityAdapter<ReminderType>>();
            query.Where(r => r.RowKey == _id.ToString());

            var entity = query.Execute().FirstOrDefault();
            return await Task.FromResult(entity?.OriginalEntity);
        }
    }
}