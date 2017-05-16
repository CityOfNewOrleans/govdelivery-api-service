using Fclp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using GovDelivery.Entity;
using GovDelivery.Csv;
using GovDelivery.Csv.Interfaces;
using GovDelivery.Entity.Models;
using GovDelivery.Csv.Models;

namespace GovDelivery.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser();

            p.Setup<string>('i', "import")
                .Callback(path =>
                {


                    var optionsBuilder = new DbContextOptionsBuilder<GovDeliveryContext>()
                        .UseSqlServer(ConfigurationManager.ConnectionStrings["GovDeliveryContext"].ConnectionString);

                    using (var context = new GovDeliveryContext(optionsBuilder.Options))
                    {

                    };
                });


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
