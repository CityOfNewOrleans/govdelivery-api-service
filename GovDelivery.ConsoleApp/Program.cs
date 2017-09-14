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
using Microsoft.EntityFrameworkCore;

namespace GovDelivery.ConsoleApp
{
    class Program
    {
        protected const string DEFAULT_HELP_OPTIONS = "-?|-h|--help";
        protected static AppSettings AppSettings { get; set; }
        protected static IGovDeliveryApiService Service { get; set; }
        protected static GovDeliveryContextFactory ContextFactory { get; set; }
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

            var builderOptions = new DbContextOptionsBuilder()
                .UseSqlServer(AppSettings.ConnectionStrings.GovDelivery);
            ContextFactory = new GovDeliveryContextFactory(builderOptions);

            Importer = new CsvImporter();

            ConfigureCli(args);
        }

        public static void ConfigureCli(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption(DEFAULT_HELP_OPTIONS);
            app.Description = "GovDelivery console app";

            app.Command("import", command => {
                command.Description = "Import subscribers from a .csv file. (email subscribers only)";
                command.HelpOption(DEFAULT_HELP_OPTIONS);
                var filePathArgument = command.Argument("[filePath]", "The path of the .csv file to be imported.");
                command.OnExecute(async () => await ImportSubscribers(filePathArgument.Value, Importer, ContextFactory));
            });

            app.Command("sync", command =>
            {
                command.HelpOption(DEFAULT_HELP_OPTIONS);

                command.Command("all", subCmd => {
                    subCmd.Description = "Sync categories, topics, and subscriptions from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await PerformFullSync(Service, ContextFactory));
                });

                command.Command("categories", subCmd => {
                    subCmd.Description = "Sync categories from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await SyncCategories(Service, ContextFactory));
                });

                command.Command("topics", subCmd => {
                    subCmd.Description = "Sync Topics from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await SyncTopics(Service, ContextFactory));
                });

                command.Command("subscribers", subCmd => {
                    subCmd.Description = "Sync subscribers from the GovDelivery system to the locab db.";
                    subCmd.HelpOption(DEFAULT_HELP_OPTIONS);
                    subCmd.OnExecute(async () => await SyncSubscribers(Service, ContextFactory));
                });                
                
            });

            app.Execute(args);
        }
        
        /// Only handles email subscribers.
        public static async Task<int> ImportSubscribers(string filePath, ICsvImporter importer, GovDeliveryContextFactory factory)
        {

            var ctx = factory.CreateDbContext();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Path to a .csv file must be provided.");
                return 1;
            }

            Console.WriteLine($"Attempting to import subscribers from {filePath}...");
            
            var subscribers = await importer.ImportSubscribersAsync(filePath);

            Console.WriteLine($"Found {subscribers.Count()} subscribers to import.");

            var importedSubscribers = subscribers
                .Where(s => s.Type == SubscriberType.Email)
                .Select(s => new Subscriber { Id = Guid.NewGuid(), Email = s.Contact });

            var localSubscribers = ctx.Subscribers.ToList();

            var newSubscribers = importedSubscribers
                .Where(s => !localSubscribers.Any(ls => ls.Email == s.Email));

            ctx.Subscribers.AddRange(newSubscribers);
            await ctx.SaveChangesAsync();

            Console.WriteLine("Successfully imported subscribers.");

            return 0;
        }

        public static async Task<int> SyncCategories(IGovDeliveryApiService service, GovDeliveryContextFactory factory) {
            Console.WriteLine("Syncing Categories...");
            await BusinessTasks.SyncCategories(service, factory.CreateDbContext());
            Console.WriteLine("Category sync successful.");

            return 0;
        }

        public static async Task<int> SyncTopics(IGovDeliveryApiService service, GovDeliveryContextFactory factory) {
            Console.WriteLine("Syncing Topics...");
            await BusinessTasks.SyncTopics(service, factory.CreateDbContext());
            Console.WriteLine("Topic sync Successful.");

            return 0;
        }

        public static async Task<int> SyncSubscribers (IGovDeliveryApiService service, GovDeliveryContextFactory factory) {
            Console.WriteLine(" Syncing Subscribers and Subscriptions...");
            await BusinessTasks.UpdateSubscribers(service, factory);
            Console.WriteLine("Subscriber sync successful.");

            return 0;
        }

        public static async Task<int> PerformFullSync(IGovDeliveryApiService service, GovDeliveryContextFactory factory)
        {
            Console.WriteLine("Beginning sync...");

            await SyncTopics(service, factory);
            await SyncCategories(service, factory);
            await SyncSubscribers(service, factory);

            Console.WriteLine("Sync successful.");

            return 0;
        }
    }
}