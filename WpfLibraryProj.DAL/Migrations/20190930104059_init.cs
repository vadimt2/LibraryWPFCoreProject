using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WpfLibraryProj.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbstractItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    RentedQuantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Disscount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    AbstractItemCategory = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    BookCategory = table.Column<int>(nullable: true),
                    ISBN = table.Column<Guid>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    JournalCategory = table.Column<int>(nullable: true),
                    Publisher = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserRank = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRentingModule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbstractItemId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsItemReturned = table.Column<bool>(nullable: false),
                    DateItemRented = table.Column<DateTime>(nullable: true),
                    DateItemReturned = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRentingModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerRentingModule_AbstractItems_AbstractItemId",
                        column: x => x.AbstractItemId,
                        principalTable: "AbstractItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRentingModule_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRentingModule_AbstractItemId",
                table: "CustomerRentingModule",
                column: "AbstractItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRentingModule_UserId",
                table: "CustomerRentingModule",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerRentingModule");

            migrationBuilder.DropTable(
                name: "AbstractItems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
