using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage.Commands
{
    public class AddOrUpdateReminderType : TableStorageCommand<ReminderType>
    {
        private readonly ReminderType _reminderType;

        public AddOrUpdateReminderType(ReminderType reminderType)
        {
            _reminderType = reminderType;
        }

        public override async Task ExecuteAsync(CloudTable context)
        {
            var adapter = new TableEntityAdapter<ReminderType>(_reminderType, _reminderType.ClientId.ToString(), _reminderType.Id.ToString());
            var operation = TableOperation.InsertOrReplace(adapter);
            await context.ExecuteAsync(operation);
        }
    }
}