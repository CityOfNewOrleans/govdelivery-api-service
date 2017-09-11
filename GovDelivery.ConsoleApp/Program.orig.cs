using GovDelivery.ConsoleApp.Configuration;
using GovDelivery.Csv;
using GovDelivery.Csv.Models;
using GovDelivery.Entity;
using GovDelivery.Entity.Models;
using GovDelivery.Logic;
using GovDelivery.Rest;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GovDelivery.Csv.Interfaces;

namespace GovDelivery.ConsoleApp
{
    class Program
    {
        protected const string DEFAULT_HELP_OPTIONS = "-?|-h|--help";
        protected static AppSettings AppSettings { get; set; }
        protected static IGovDeliveryApiService Service { get; set; }
        protected static IGovDeliveryContext Context { get; set; }
        protected static ICsvImporter Importer {get;set;}

        static void Main(string[] args)
        {
            using (var reader = File.OpenText($@"{AppContext.BaseDirectory}\appSettings.json"))
            {
                var appSettingsText = reader.ReadToEnd();
                AppSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsText);
            }

            var baseUri = (AppSettings.GovDelivery.Server == GovDeliveryServer.Main)
                ? GovDeliveryApiService.MAIN_URI
                : GovDeliveryApiService.STAGING_URI;

            Service = new GovDeliveryApiService(
                baseUri, 
                AppSettings.GovDelivery.AccountCode,
                AppSettings.GovDelivery.Username,
                AppSettings.GovDelivery.Password
            );

            Context = new GovDeliveryContext();

            Importer = new CsvImporter();

            ConfigureCli(args);
        }

        public static void ConfigureCli(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption(DEFAULT_HELP_OPTIONS);
            app.Description = "GovDelivery console app";

            app.Command("import", command => {
                command.Description = "Import subscribers from a .csv file.";
                command.HelpOption(DEFAULT_HELP_OPTIONS);
                var filePathArgument = command.Argument("[filePath]", "The path of the .csv file to be imported.");
                command.OnExecute(async () => await ImportSubscribers(filePathArgument.Value, Importer, Context));
            });

            app.Command("sync", command =>
            {
                command.HelpOption(DEFAULT_HELP_OPTIONS);

                command.Command("all", subCmd => {
                    subCmd.Description = "Sync categories, topics, and subscriptions from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await PerformFullSync(Service, Context));
                });

                command.Command("categories", subCmd => {
                    subCmd.Description = "Sync categories from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await SyncCategories(Service, Context));
                });

                command.Command("topics", subCmd => {
                    subCmd.Description = "Sync Topics from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await SyncTopics(Service, Context));
                });

                command.Command("subscribers", subCmd => {
                    subCmd.Description = "Sync subscribers from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await SyncSubscribers(Service, Context));
                });                
                
            });

            app.Execute(args);
        }
        
        public static async Task<int> ImportSubscribers(string filePath, ICsvImporter importer, IGovDeliveryContext ctx)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Path to a .csv file must be provided.");
                return 1;
            }

            Console.WriteLine($"Attempting to import subscribers from {filePath}...");
            
            var subscribers = await importer.ImportSubscribersAsync(filePath);

            Console.WriteLine($"Found {subscribers.Count()} subscribers to import.");

            var entities = subscribers.Select(s => new Subscriber
            {
                Id = Guid.NewGuid(),
                Email = s.Type == SubscriberType.Email ? s.Contact : null,
                Phone = s.Type == SubscriberType.Phone ? s.Contact : null,
            });

            ctx.AddRange(entities);
            await ctx.SaveChangesAsync();

            Console.WriteLine("Successfully imported subscribers.");

            return 0;
        }

        public static async Task<int> SyncCategories(IGovDeliveryApiService service, IGovDeliveryContext ctx) {
            Console.WriteLine("Syncing Categories...");
            await BusinessTasks.SyncCategories(service, ctx);
            Console.WriteLine("Category sync successful.");

            return 0;
        }

        public static async Task<int> SyncTopics(IGovDeliveryApiService service, IGovDeliveryContext ctx) {
            Console.WriteLine("Syncing Topics...");
            await BusinessTasks.SyncTopics(service, ctx);
            Console.WriteLine("Topic sync Successful.");

            return 0;
        }

        public static async Task<int> SyncSubscribers (IGovDeliveryApiService service, IGovDeliveryContext ctx) {
            Console.WriteLine(" Syncing Subscribers and Subscriptions...");
            await BusinessTasks.UpdateSubscribers(service, ctx);
            Console.WriteLine("Subscriber sync successful.");

            return 0;
        }

        public static async Task<int> PerformFullSync(IGovDeliveryApiService service, IGovDeliveryContext ctx)
        {
            Console.WriteLine("Beginning sync...");

            await SyncTopics(service, ctx);
            await SyncCategories(service, ctx);
            await SyncSubscribers(service, ctx);

            Console.WriteLine("Sync successful.");

            return 0;
        }
    }
}