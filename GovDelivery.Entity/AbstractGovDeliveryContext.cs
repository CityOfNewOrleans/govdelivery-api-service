using GovDelivery.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace GovDelivery.Entity
{
    public abstract class AbstractGovDeliveryContext : DbContext, IGovDeliveryContext
    {
        public AbstractGovDeliveryContext():base() { }

        public AbstractGovDeliveryContext(DbContextOptions dbOptions):base(dbOptions) { }

        public DbSet<EmailSubscriber> Subscribers { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Page> Pages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories);

            ConfigureTopicCategories(modelBuilder);

            ConfigureEmailSubscriberTopics(modelBuilder);

            ConfigureEmailSubscriberCategories(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        protected static void ConfigureTopicCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicCategory>()
                .HasKey(e => new { e.TopicId, e.CategoryId});

            modelBuilder.Entity<TopicCategory>()
                .HasOne(tc => tc.Topic)
                .WithMany(t => t.TopicCategories)
                .HasForeignKey(tc => tc.TopicId);

            modelBuilder.Entity<TopicCategory>()
                .HasOne(tc => tc.Category)
                .WithMany(c => c.TopicCategories)
                .HasForeignKey(tc => tc.CategoryId);
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

        protected static void ConfigureEmailSubscriberCategories(ModelBuilder modelBulder)
        {
            modelBulder.Entity<EmailSubscriberCategory>()
                .HasKey(esc => new { esc.EmailSubscriberId, esc.CategoryId });

            modelBulder.Entity<EmailSubscriberCategory>()
                .HasOne(esc => esc.EmailSubscriber)
                .WithMany(es => es.EmailSubscriberCategories)
                .HasForeignKey(esc => esc.EmailSubscriberId);

            modelBulder.Entity<EmailSubscriberCategory>()
                .HasOne(esc => esc.Category)
                .WithMany(c => c.EmailSubscriberCategories)
                .HasForeignKey(esc => esc.CategoryId);
        }

    }
}
