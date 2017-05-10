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
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories);

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.ParentCategory)
                .WithMany(c => c.Topics);


            ConfigureEmailSubscriberTopics(modelBuilder);


            base.OnModelCreating(modelBuilder);
        }

        protected static void ConfigureEmailSubscriberTopics(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailSubscriberTopic>()
                .HasKey(e => new { e.EmailSubscriberId, e.TopicId });

            modelBuilder.Entity<EmailSubscriberTopic>()
                .HasOne(est => est.EmailSubscriber)
                .WithMany(e => e.EmailSubscriberTopics)
                .HasForeignKey(est => est.EmailSubscriberId);

            modelBuilder.Entity<EmailSubscriberTopic>()
                .HasOne(est => est.Topic)
                .WithMany(t => t.EmailSubscriberTopics)
                .HasForeignKey(est => est.TopicId);
        }

    }
}
