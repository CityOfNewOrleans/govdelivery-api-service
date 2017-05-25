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
using System.Threading.Tasks;

namespace GovDelivery.ConsoleApp
{
    class Program
    {
        protected const string DEFAULT_HELP_OPTIONS = "-?|-h|--help";
        protected static AppSettings AppSettings { get; set; }

        static void Main(string[] args)
        {
            using (var reader = File.OpenText("appSettings.json"))
            {
                var appSettingsText = reader.ReadToEnd();
                AppSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsText);
            }
        }

        public static void ConfigureCli(string[] args)
        {
            var app = new CommandLineApplication();

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