using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arib.EmployeeTaskManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addiconcoltoTaskStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "TaskStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "TaskStatus");
        }
    }
}
