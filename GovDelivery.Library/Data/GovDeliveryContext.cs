using GovDelivery.Data.Entities;
using System.Data.Entity;

namespace CityBusiness.Data
{
    public class GovDeliveryContext : DbContext
    {
        public GovDeliveryContext():base("GovDeliveryContext") { }

        public DbSet<EmailSubscriber> Subscribers { get; set; }

        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }

    }
}
