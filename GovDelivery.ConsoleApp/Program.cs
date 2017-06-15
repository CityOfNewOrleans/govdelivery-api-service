using GovDelivery.ConsoleApp.Configuration;
using GovDelivery.Csv;
using GovDelivery.Csv.Models;
using GovDelivery.Entity;
using GovDelivery.Entity.Models;
using GovDelivery.Rest;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GovDelivery.ConsoleApp
{
    class Program
    {
        protected const string DEFAULT_HELP_OPTIONS = "-?|-h|--help";
        protected static AppSettings AppSettings { get; set; }

        static void Main(string[] args)
        {
            using (var reader = File.OpenText($@"{AppContext.BaseDirectory}\appSettings.json"))
            {
                var appSettingsText = reader.ReadToEnd();
                AppSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsText);
            }

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

                command.OnExecute(() =>
                {
                    if (string.IsNullOrWhiteSpace(filePathArgument.Value))
                    {
                        Console.WriteLine("Path to a .csv file must be provided.");
                        return 1;
                    }

                    Console.WriteLine($"Attempting to import subscribers from {filePathArgument.Value}...");

                    ImportSubscribers(filePathArgument.Value, new GovDeliveryContext());

                    Console.WriteLine("Successfully imported subscribers.");

                    return 0;
                });

            });

            app.Command("sync", command =>
            {
                command.Description = "Sync categories, topics, and subscriptions from the GovDelivery system to the locab db.";

                command.HelpOption(DEFAULT_HELP_OPTIONS);

                command.OnExecute(() => 
                {
                    Console.WriteLine("Beginning sync...");

                    var service = new GovDeliveryApiService(
                        AppSettings.GovDelivery.Server == GovDeliveryServer.Main ? GovDeliveryApiService.MAIN_URI : GovDeliveryApiService.STAGING_URI, 
                        AppSettings.GovDelivery.AccountCode,
                        AppSettings.GovDelivery.Username,
                        AppSettings.GovDelivery.Password
                    );

                    var ctx = new GovDeliveryContext();

                    var topicsResult = service.ListTopicsAsync().Result;

                    if (!topicsResult.HttpResponse.IsSuccessStatusCode)
                    {
                        Console.Error.WriteLine($@"Error getting Topics: {topicsResult.HttpResponse.StatusCode} - {topicsResult.HttpResponse.ReasonPhrase}");
                    }

                    Console.WriteLine($"Fetched {topicsResult.Data.Items.Count()} topics.");

                    var topicEntities = topicsResult.Data.Items
                        .Select(i => new Topic {
                            Id = Guid.NewGuid(),
                            Code = i.Code,
                            Description = i.Description.Value,
                   Name = i.Name,
                            ShortName = i.ShortName,
                            WirelessEnabled = i.WirelessEnabled.Value
                        })
                        .ToList();

                    ctx.AddRange(topicEntities);
                    ctx.SaveChanges();

                    var categoriesResult = service.ListCategoriesAsync().Result;

                    var categoryEntities = categoriesResult.Data.Items
                        .Select(i => new Category
                        {
                            Id = Guid.NewGuid(),
                            Code = i.Code,
                            Description = i.Description,
                            DefaultOpen = i.DefaultOpen.Value,
                            AllowUserInitiatedSubscriptions = i.AllowSubscriptions.Value,
                            Name = i.Name,
                            ShortName = i.ShortName,
                        });

                    ctx.Add(categoryEntities);
                    ctx.SaveChanges();

                    return 0;
                });

            });

            app.Execute(args);
        }

        public static void ImportSubscribers(string filePath, GovDeliveryContext ctx)
        {
            var importer = new CsvImporter();

            var subscribers = importer.ImportSubscribersAsync(filePath).Result;

            Console.WriteLine($"Found {subscribers.Count()} subscribers to import.");

            var entities = subscribers.Select(s => new EmailSubscriber
            {
                Id = Guid.NewGuid(),
                Email = s.Type == SubscriberType.Email ? s.Contact : null,
                Phone = s.Type == SubscriberType.Phone ? s.Contact : null,
            });

            ctx.AddRange(entities);

            ctx.SaveChanges();
        }

        public static void PullSubscriberData()
        {
            var govDeliveryService = new GovDeliveryApiService(
                GovDeliveryApiService.MAIN_URI, 
                AppSettings.GovDelivery.AccountCode,
                AppSettings.GovDelivery.Username,
                AppSettings.GovDelivery.Password
            );
        }
    }
}