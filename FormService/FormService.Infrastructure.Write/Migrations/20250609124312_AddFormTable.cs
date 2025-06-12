using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormService.Infrastructure.Write.Migrations
{
    /// <inheritdoc />
    public partial class AddFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormTemplate",
                columns: table => new
                {
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormTemplateData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplate", x => x.FormTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerForm",
                columns: table => new
                {
                    CustomerFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerFormData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerForm", x => x.CustomerFormId);
                    table.ForeignKey(
                        name: "FK_CustomerForm_FormTemplate",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplate",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerForm_FormTemplateId",
                table: "CustomerForm",
                column: "FormTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerForm");

            migrationBuilder.DropTable(
                name: "FormTemplate");
        }
    }
}
