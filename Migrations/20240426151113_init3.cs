using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecomove_back.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d029834e-57a7-49f5-a600-0784db9cf3f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5c13d13-558e-4b0a-b6e2-e2a6fe7cfe15");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20c536b8-da1f-4a42-8921-f11e21b04888", null, "ADMIN", "ADMIN" },
                    { "2884574e-0796-4813-b157-24131b4cfdb1", null, "USER", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20c536b8-da1f-4a42-8921-f11e21b04888");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2884574e-0796-4813-b157-24131b4cfdb1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d029834e-57a7-49f5-a600-0784db9cf3f1", null, "ADMIN", null },
                    { "d5c13d13-558e-4b0a-b6e2-e2a6fe7cfe15", null, "USER", null }
                });
        }
    }
}
