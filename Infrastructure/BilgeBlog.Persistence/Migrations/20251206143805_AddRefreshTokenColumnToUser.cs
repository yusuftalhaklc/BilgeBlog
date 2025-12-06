using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilgeBlog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 6, 14, 38, 4, 945, DateTimeKind.Utc).AddTicks(8410));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 6, 14, 38, 4, 945, DateTimeKind.Utc).AddTicks(8414));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 6, 14, 38, 4, 945, DateTimeKind.Utc).AddTicks(8416));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 6, 11, 45, 46, 848, DateTimeKind.Utc).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 6, 11, 45, 46, 848, DateTimeKind.Utc).AddTicks(5274));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedDate",
                value: new DateTime(2025, 12, 6, 11, 45, 46, 848, DateTimeKind.Utc).AddTicks(5276));
        }
    }
}
