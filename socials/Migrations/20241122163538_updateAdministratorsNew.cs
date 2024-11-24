using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class updateAdministratorsNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("2b4d3b8b-f3ae-4b9a-9456-cec31003f7fa") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new Guid("2b4d3b8b-f3ae-4b9a-9456-cec31003f7fa") });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 35, 38, 101, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 35, 38, 101, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 35, 38, 101, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 35, 38, 101, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") },
                column: "Role",
                value: 1);

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 },
                    { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 32, 8, 552, DateTimeKind.Utc).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 32, 8, 552, DateTimeKind.Utc).AddTicks(1970));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 32, 8, 552, DateTimeKind.Utc).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 32, 8, 552, DateTimeKind.Utc).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") },
                column: "Role",
                value: 0);

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("2b4d3b8b-f3ae-4b9a-9456-cec31003f7fa"), 1 },
                    { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new Guid("2b4d3b8b-f3ae-4b9a-9456-cec31003f7fa"), 0 }
                });
        }
    }
}
