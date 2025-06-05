using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Infrastructure.Write.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountUsername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountFullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountDob = table.Column<DateOnly>(type: "date", nullable: true),
                    AccountGender = table.Column<int>(type: "int", nullable: false),
                    AccountPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    AccountImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountTicketRequest = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
