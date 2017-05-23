using GovDelivery.ConsoleApp.Configuration;
using GovDelivery.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GovDelivery.ConsoleApp
{
    public class GovDeliveryContext : AbstractGovDeliveryContext
    {
        protected AppSettings AppSettings { get; set; }

        public GovDeliveryContext () : base()
        {
            using (var reader = File.OpenText("appSettings.json")) {
                var settingsText = reader.ReadToEnd();

                AppSettings = JsonConvert.DeserializeObject<AppSettings>(settingsText);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionStrings.GovDelivery);
        }
    }
}
