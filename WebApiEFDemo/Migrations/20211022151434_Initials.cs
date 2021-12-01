using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDemo.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Products",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Products", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Tokens",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AuthToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
            //        IssuedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ExpiredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        userId = table.Column<int>(type: "int", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tokens", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Tokens_Users_userId",
            //            column: x => x.userId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tokens_userId",
            //    table: "Tokens",
            //    column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
