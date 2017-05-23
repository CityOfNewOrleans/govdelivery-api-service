using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GovDelivery.ConsoleApp;
using GovDelivery.Entity.Models;

namespace GovDelivery.ConsoleApp.Migrations
{
    [DbContext(typeof(GovDeliveryContext))]
    partial class GovDeliveryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GovDelivery.Entity.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowUserInitiatedSubscriptions");

                    b.Property<string>("Code");

                    b.Property<bool>("DefaultOpen");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("ParentCategoryId");

                    b.Property<string>("QuickSubscribePageCode");

                    b.Property<string>("ShortName")
                        .IsRequired();

                    b.Property<Guid?>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("TopicId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.EmailSubscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BulletinFrequency");

                    b.Property<string>("CountryCode");

                    b.Property<string>("Email");

                    b.Property<int>("GovDeliveryId");

                    b.Property<string>("Link");

                    b.Property<string>("Phone");

                    b.Property<bool>("SendSubscriberUpdateNotifications");

                    b.HasKey("Id");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.EmailSubscriberCategory", b =>
                {
                    b.Property<Guid>("EmailSubscriberId");

                    b.Property<Guid>("CategoryId");

                    b.HasKey("EmailSubscriberId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("EmailSubscriberCategory");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.EmailSubscriberTopic", b =>
                {
                    b.Property<Guid>("EmailSubscriberId");

                    b.Property<Guid>("TopicId");

                    b.HasKey("EmailSubscriberId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("EmailSubscriberTopic");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.Page", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("TopicId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<int>("DefaultPageWatchResults");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<bool>("PageWatchAutosend");

                    b.Property<bool>("PageWatchEnabled");

                    b.Property<bool>("PageWatchSuspended");

                    b.Property<int?>("PagewatchType");

                    b.Property<string>("RssFeedDescription");

                    b.Property<string>("RssFeedTitle");

                    b.Property<string>("RssFeedUrl");

                    b.Property<bool>("SendByEmailEnabled");

                    b.Property<string>("ShortName");

                    b.Property<bool>("WatchTaggedContent");

                    b.Property<bool>("WirelessEnabled");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.TopicCategory", b =>
                {
                    b.Property<Guid>("TopicId");

                    b.Property<Guid>("CategoryId");

                    b.HasKey("TopicId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("TopicCategory");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.Category", b =>
                {
                    b.HasOne("GovDelivery.Entity.Models.Category", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId");

                    b.HasOne("GovDelivery.Entity.Models.Topic")
                        .WithMany("Categories")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.EmailSubscriberCategory", b =>
                {
                    b.HasOne("GovDelivery.Entity.Models.Category", "Category")
                        .WithMany("EmailSubscriberCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GovDelivery.Entity.Models.EmailSubscriber", "EmailSubscriber")
                        .WithMany("EmailSubscriberCategories")
                        .HasForeignKey("EmailSubscriberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.EmailSubscriberTopic", b =>
                {
                    b.HasOne("GovDelivery.Entity.Models.EmailSubscriber", "EmailSubscriber")
                        .WithMany("EmailSubscriberTopics")
                        .HasForeignKey("EmailSubscriberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GovDelivery.Entity.Models.Topic", "Topic")
                        .WithMany("EmailSubscriberTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.Page", b =>
                {
                    b.HasOne("GovDelivery.Entity.Models.Topic")
                        .WithMany("Pages")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("GovDelivery.Entity.Models.TopicCategory", b =>
                {
                    b.HasOne("GovDelivery.Entity.Models.Category", "Category")
                        .WithMany("TopicCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GovDelivery.Entity.Models.Topic", "Topic")
                        .WithMany("TopicCategories")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
