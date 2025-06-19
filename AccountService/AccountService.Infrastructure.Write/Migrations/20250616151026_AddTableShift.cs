using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Infrastructure.Write.Migrations
{
    /// <inheritdoc />
    public partial class AddTableShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LawyerDayOffSchedule",
                columns: table => new
                {
                    LawyerDayOffScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffDay = table.Column<DateOnly>(type: "date", nullable: false),
                    LawyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerDayOffSchedule", x => x.LawyerDayOffScheduleId);
                    table.ForeignKey(
                        name: "FK_LawyerDayOffSchedule_Account",
                        column: x => x.LawyerId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "SpecificLawyerDayOffSchedule",
                columns: table => new
                {
                    LawyerDayOffScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificLawyerDayOffSchedule", x => new { x.LawyerDayOffScheduleId, x.ShiftId });
                    table.ForeignKey(
                        name: "FK_SpecificLawyerDayOffSchedule_LawyerDayOffSchedule",
                        column: x => x.LawyerDayOffScheduleId,
                        principalTable: "LawyerDayOffSchedule",
                        principalColumn: "LawyerDayOffScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificLawyerDayOffSchedule_Shift",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LawyerDayOffSchedule_LawyerId",
                table: "LawyerDayOffSchedule",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificLawyerDayOffSchedule_ShiftId",
                table: "SpecificLawyerDayOffSchedule",
                column: "ShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecificLawyerDayOffSchedule");

            migrationBuilder.DropTable(
                name: "LawyerDayOffSchedule");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
