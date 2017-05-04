using GovDelivery.Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GovDelivery.Library.Data
{
    public class GovDeliveryContext : DbContext
    {
        public GovDeliveryContext():base() { }

        public GovDeliveryContext(DbContextOptions dbOptions):base(dbOptions) { }

        public DbSet<EmailSubscriber> Subscribers { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
