using Demo.Persistence.Core;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Demo.Persistence.TableStorage
{
    public class TableStorageRepository : IRepository<CloudTable>
    {
        private readonly CloudStorageAccount _account;

        public TableStorageRepository(string connectionString, TableStorageOptions options)
        {
            _account = CreateStorageAccountFromConnectionString(connectionString, options);
        }

        public async Task ExecuteAsync<T>(ICommand<CloudTable, T> command)
        {
            var table = await GetTableAsync<ReminderType>();
            await command.ExecuteAsync(table);
        }

        public async Task<IEnumerable<T>> FindAsync<T>(IQuery<CloudTable, T> query)
        {
            var table = await GetTableAsync<T>();
            return await query.ExecuteAsync(table);
        }

        public async Task<T> FindAsync<T>(IScalar<CloudTable, T> query)
        {
            var table = await GetTableAsync<T>();
            return await query.ExecuteAsync(table);
        }

        private CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString, TableStorageOptions options)
        {
            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                var tableServicePoint = ServicePointManager.FindServicePoint(storageAccount.TableEndpoint);
                tableServicePoint.UseNagleAlgorithm = options.UseNagleAlgorithm;
                tableServicePoint.Expect100Continue = options.Expect100Continue;
                tableServicePoint.ConnectionLimit = options.ConnectionLimit;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public async Task<CloudTable> GetTableAsync<T>()
        {
            var tableName = typeof(T).Name;
            var tableClient = _account.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);

            //TODO:  make this an option
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}