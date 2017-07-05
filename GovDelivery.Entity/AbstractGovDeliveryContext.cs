using System;
using GovDelivery.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace GovDelivery.Entity
{
    public abstract class AbstractGovDeliveryContext : DbContext, GovDeliveryContext
    {
        public AbstractGovDeliveryContext():base() { }

        public AbstractGovDeliveryContext(DbContextOptions dbOptions):base(dbOptions) { }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<CategorySubscription> CategorySubscriptions { get; set; }

        public DbSet<TopicSubscription> TopicSubscriptions { get; set; }

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
            modelBuilder.Entity<TopicSubscription>()
                .HasKey(e => new { e.SubscriberId, e.TopicId });

            modelBuilder.Entity<TopicSubscription>()
                .HasOne(est => est.Subscriber)
                .WithMany(e => e.TopicSubscriptions)
                .HasForeignKey(est => est.SubscriberId);

            modelBuilder.Entity<TopicSubscription>()
                .HasOne(est => est.Topic)
                .WithMany(t => t.EmailSubscriberTopics)
                .HasForeignKey(est => est.TopicId);
        }

        protected static void ConfigureEmailSubscriberCategories(ModelBuilder modelBulder)
        {
            modelBulder.Entity<CategorySubscription>()
                .HasKey(esc => new { esc.SubscriberId, esc.CategoryId });

            modelBulder.Entity<CategorySubscription>()
                .HasOne(esc => esc.Subscriber)
                .WithMany(es => es.CategorySubscriptions)
                .HasForeignKey(esc => esc.SubscriberId);

            modelBulder.Entity<CategorySubscription>()
                .HasOne(esc => esc.Category)
                .WithMany(c => c.EmailSubscriberCategories)
                .HasForeignKey(esc => esc.CategoryId);
        }

    }
}
