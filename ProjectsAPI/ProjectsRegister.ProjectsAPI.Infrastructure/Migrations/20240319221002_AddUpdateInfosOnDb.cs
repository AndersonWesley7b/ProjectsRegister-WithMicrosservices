using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateInfosOnDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Projects");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Projects",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
