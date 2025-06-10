using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketService.Infrastructure.Write.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTicketPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketPackage",
                columns: table => new
                {
                    TicketPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketPackageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestAmount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPackage", x => x.TicketPackageId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPackage");
        }
    }
}
