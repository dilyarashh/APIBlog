using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class commentsPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 38, 14, 254, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 38, 14, 254, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 38, 14, 254, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 38, 14, 254, DateTimeKind.Utc).AddTicks(5420));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 11, 56, 333, DateTimeKind.Utc).AddTicks(7040));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 11, 56, 333, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 11, 56, 333, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 2, 10, 11, 56, 333, DateTimeKind.Utc).AddTicks(7050));
        }
    }
}
