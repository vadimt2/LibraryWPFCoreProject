using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfLibraryProj.DAL.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbstractItemCategoryId",
                table: "AbstractItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookCategoryId",
                table: "AbstractItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JournalCategoryId",
                table: "AbstractItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbstractItemCategoryId",
                table: "AbstractItems");

            migrationBuilder.DropColumn(
                name: "BookCategoryId",
                table: "AbstractItems");

            migrationBuilder.DropColumn(
                name: "JournalCategoryId",
                table: "AbstractItems");
        }
    }
}
