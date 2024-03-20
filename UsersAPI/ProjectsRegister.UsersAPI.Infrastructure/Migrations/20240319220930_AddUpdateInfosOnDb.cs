using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsRegister.UsersAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateInfosOnDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Users");
        }
    }
}
