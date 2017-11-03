using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ngScaffoldingdatabase.Migrations
{
    public partial class InitialVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemOrder = table.Column<int>(type: "int", nullable: true),
                    JsonSerialized = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMenuItemId = table.Column<int>(type: "int", nullable: true),
                    badge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    badgeStyleClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    command = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disabled = table.Column<bool>(type: "bit", nullable: false),
                    expanded = table.Column<bool>(type: "bit", nullable: false),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    routerLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    routerLinkActiveOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    separator = table.Column<bool>(type: "bit", nullable: false),
                    style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    styleClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems_ParentMenuItemId",
                        column: x => x.ParentMenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Authorisation = table.Column<bool>(type: "bit", nullable: false),
                    CacheSeconds = table.Column<int>(type: "int", nullable: true),
                    ConnectionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InputDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InputDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceValueItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemOrder = table.Column<int>(type: "int", nullable: true),
                    ReferenceValueId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceValueItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceValueItems_ReferenceValues_ReferenceValueId",
                        column: x => x.ReferenceValueId,
                        principalTable: "ReferenceValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferenceValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PreferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPreferenceId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferenceValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPreferenceValues_UserPreferences_UserPreferenceId",
                        column: x => x.UserPreferenceId,
                        principalTable: "UserPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentMenuItemId",
                table: "MenuItems",
                column: "ParentMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceValueItems_ReferenceValueId",
                table: "ReferenceValueItems",
                column: "ReferenceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferenceValues_UserPreferenceId",
                table: "UserPreferenceValues",
                column: "UserPreferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "ReferenceValueItems");

            migrationBuilder.DropTable(
                name: "UserPreferenceValues");

            migrationBuilder.DropTable(
                name: "ReferenceValues");

            migrationBuilder.DropTable(
                name: "UserPreferences");
        }
    }
}
