using GovDelivery.Csv;
using GovDelivery.Csv.Models;
using GovDelivery.Entity;
using GovDelivery.Entity.Models;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Linq;

namespace GovDelivery.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();



        }

        public async void SaveSubscribers(string filePath, GovDeliveryContext ctx)
        {
            var importer = new CsvImporter();

            var subscribers = await importer.ImportSubscribersAsync(filePath);

            var entities = subscribers.Select(s => new EmailSubscriber
            {
                Id = Guid.NewGuid(),
                Email = s.Type == SubscriberType.Email ? s.Contact : null,
                Phone = s.Type == SubscriberType.Phone ? s.Contact : null,
            });

            ctx.AddRange(entities);
        }
    }
}