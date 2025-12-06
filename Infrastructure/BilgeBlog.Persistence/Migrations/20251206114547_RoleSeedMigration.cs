using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BilgeBlog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 12, 6, 11, 45, 46, 848, DateTimeKind.Utc).AddTicks(5271), null, "Admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 12, 6, 11, 45, 46, 848, DateTimeKind.Utc).AddTicks(5274), null, "Author", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 12, 6, 11, 45, 46, 848, DateTimeKind.Utc).AddTicks(5276), null, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));
        }
    }
}
