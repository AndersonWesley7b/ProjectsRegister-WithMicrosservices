using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameToProjectsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Projects");
        }
    }
}
