using GovDelivery.ConsoleApp.Configuration;
using GovDelivery.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.ConsoleApp
{
    public class GovDeliveryContextFactory : AbstractGovDeliveryContextFactory<GovDeliveryContext>
    {

        public GovDeliveryContextFactory()
        {
            AppSettings appSettings;
            using (var reader = File.OpenText($@"{AppContext.BaseDirectory}\appSettings.json"))
            {
                var settingsText = reader.ReadToEnd();

                appSettings = JsonConvert.DeserializeObject<AppSettings>(settingsText);
            }

            optionsBuilder = new DbContextOptionsBuilder()
                .UseSqlServer(appSettings.ConnectionStrings.GovDelivery);
        }

        public GovDeliveryContextFactory(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder) { }
    }
}
