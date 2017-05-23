using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GovDelivery.ConsoleApp.Migrations
{
    public partial class IntitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BulletinFrequency = table.Column<int>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    GovDeliveryId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SendSubscriberUpdateNotifications = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    DefaultPageWatchResults = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PageWatchAutosend = table.Column<bool>(nullable: false),
                    PageWatchEnabled = table.Column<bool>(nullable: false),
                    PageWatchSuspended = table.Column<bool>(nullable: false),
                    PagewatchType = table.Column<int>(nullable: true),
                    RssFeedDescription = table.Column<string>(nullable: true),
                    RssFeedTitle = table.Column<string>(nullable: true),
                    RssFeedUrl = table.Column<string>(nullable: true),
                    SendByEmailEnabled = table.Column<bool>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    WatchTaggedContent = table.Column<bool>(nullable: false),
                    WirelessEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AllowUserInitiatedSubscriptions = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    DefaultOpen = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ParentCategoryId = table.Column<Guid>(nullable: true),
                    QuickSubscribePageCode = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailSubscriberTopic",
                columns: table => new
                {
                    EmailSubscriberId = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSubscriberTopic", x => new { x.EmailSubscriberId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_EmailSubscriberTopic_Subscribers_EmailSubscriberId",
                        column: x => x.EmailSubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailSubscriberTopic_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailSubscriberCategory",
                columns: table => new
                {
                    EmailSubscriberId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSubscriberCategory", x => new { x.EmailSubscriberId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_EmailSubscriberCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailSubscriberCategory_Subscribers_EmailSubscriberId",
                        column: x => x.EmailSubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicCategory",
                columns: table => new
                {
                    TopicId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicCategory", x => new { x.TopicId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_TopicCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicCategory_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TopicId",
                table: "Categories",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSubscriberCategory_CategoryId",
                table: "EmailSubscriberCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSubscriberTopic_TopicId",
                table: "EmailSubscriberTopic",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_TopicId",
                table: "Pages",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicCategory_CategoryId",
                table: "TopicCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSubscriberCategory");

            migrationBuilder.DropTable(
                name: "EmailSubscriberTopic");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "TopicCategory");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
