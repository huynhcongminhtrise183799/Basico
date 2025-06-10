using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketService.Infrastructure.Read.Migrations
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
                    TicketPackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    TicketPackageName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RequestAmount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
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
