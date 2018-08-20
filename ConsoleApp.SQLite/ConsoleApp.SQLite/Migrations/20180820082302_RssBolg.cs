using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.SQLite.Migrations
{
    public partial class RssBolg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Blogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RssUrl",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Url",
                table: "Blogs",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blogs_Url",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "RssUrl",
                table: "Blogs");
        }
    }
}
