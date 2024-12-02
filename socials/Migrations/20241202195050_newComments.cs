using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class newComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 19, 50, 50, 256, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 19, 50, 50, 256, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 19, 50, 50, 256, DateTimeKind.Utc).AddTicks(9810));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 19, 50, 50, 256, DateTimeKind.Utc).AddTicks(9800));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 47, 41, 516, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 47, 41, 516, DateTimeKind.Utc).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 47, 41, 516, DateTimeKind.Utc).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 47, 41, 516, DateTimeKind.Utc).AddTicks(2910));
        }
    }
}
