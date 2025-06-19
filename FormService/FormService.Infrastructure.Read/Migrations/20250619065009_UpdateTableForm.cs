using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormService.Infrastructure.Read.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "FormTemplate",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "FormTemplate");
        }
    }
}
