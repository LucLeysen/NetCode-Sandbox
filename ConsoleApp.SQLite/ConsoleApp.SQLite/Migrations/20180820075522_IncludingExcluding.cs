using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.SQLite.Migrations
{
    public partial class IncludingExcluding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntry");

            migrationBuilder.AddColumn<int>(
                name: "TypeBlogMetaDataId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogMetaData",
                columns: table => new
                {
                    BlogMetaDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoadedFromDatabase = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogMetaData", x => x.BlogMetaDataId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_TypeBlogMetaDataId",
                table: "Blogs",
                column: "TypeBlogMetaDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogMetaData_TypeBlogMetaDataId",
                table: "Blogs",
                column: "TypeBlogMetaDataId",
                principalTable: "BlogMetaData",
                principalColumn: "BlogMetaDataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogMetaData_TypeBlogMetaDataId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogMetaData");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_TypeBlogMetaDataId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TypeBlogMetaDataId",
                table: "Blogs");

            migrationBuilder.CreateTable(
                name: "AuditEntry",
                columns: table => new
                {
                    AuditEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntry", x => x.AuditEntryId);
                });
        }
    }
}
