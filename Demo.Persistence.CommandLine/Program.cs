using Demo.Persistence.Core;
using Demo.Persistence.TableStorage;
using Demo.Persistence.TableStorage.Commands;
using Demo.Persistence.TableStorage.Queries;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Persistence.CommandLine
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var repository = GetRepository(configuration["AzureStorage"]);

            var reminderTypes = await repository.FindAsync(new AllReminderTypes());
            await RenderAsync(reminderTypes);

            var reminderType = await repository.FindAsync(new ReminderTypeById(new Guid("8806fad8-8b4e-4050-9e60-1a8bc0812c95")));
            await RenderAsync(reminderType);

            var newReminderType = new ReminderType
            {
                ClientId = 1203,
                Id = Guid.NewGuid(),
                Name = $"Test Reminder Type {Guid.NewGuid().GetHashCode()}"
            };
            await repository.ExecuteAsync(new AddOrUpdateReminderType(newReminderType));

            reminderTypes = await repository.FindAsync(new AllReminderTypes());
            await RenderAsync(reminderTypes);
        }

        static IRepository<CloudTable> GetRepository(string connectionString)
        {
            var options = new TableStorageOptions();
            var repository = new TableStorageRepository(connectionString, options);
            return repository;
        }

        static async Task RenderAsync(IEnumerable<ReminderType> reminderTypes)
        {
            foreach (var reminderType in reminderTypes)
            {
                await RenderAsync(reminderType);
            }
        }

        static async Task RenderAsync(ReminderType reminderType)
        {
            await Console.Out.WriteLineAsync($"{reminderType.ClientId}:  {reminderType.Id} - {reminderType.Name}");
        }
    }
}
