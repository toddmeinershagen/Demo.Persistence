using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;

namespace Demo.Persistence.TableStorage
{
    public class DeepTableEntityAdapter<T> : TableEntityAdapter<T>
    {
        public DeepTableEntityAdapter(T originalEntity, string partitionKey, string rowKey) : base(originalEntity, partitionKey, rowKey)
        { }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            var entity = new DynamicTableEntity(PartitionKey, RowKey);
            entity.Properties = Flatten(this, operationContext);
            EntityPropertyConverter.ConvertBack<T>(entity.Properties, operationContext);
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var entity = new DynamicTableEntity(PartitionKey, RowKey);
            entity.Properties = EntityPropertyConverter.Flatten(this, operationContext);
            return entity.Properties;
        }
    }
}