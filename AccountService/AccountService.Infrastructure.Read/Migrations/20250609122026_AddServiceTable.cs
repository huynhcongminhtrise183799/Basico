using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Infrastructure.Read.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutLawyer",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ForgotPassword",
                columns: table => new
                {
                    ForgotPasswordId = table.Column<Guid>(type: "uuid", nullable: false),
                    OTP = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPassword", x => x.ForgotPasswordId);
                    table.ForeignKey(
                        name: "FK_ForgotPassword_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ServiceDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "LawyerSpecificService",
                columns: table => new
                {
                    LawyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerHour = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerSpecificService", x => new { x.LawyerId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_LawyerSpecificService_Account",
                        column: x => x.LawyerId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawyerSpecificService_Service",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForgotPassword_AccountId",
                table: "ForgotPassword",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerSpecificService_ServiceId",
                table: "LawyerSpecificService",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForgotPassword");

            migrationBuilder.DropTable(
                name: "LawyerSpecificService");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropColumn(
                name: "AboutLawyer",
                table: "Account");
        }
    }
}
