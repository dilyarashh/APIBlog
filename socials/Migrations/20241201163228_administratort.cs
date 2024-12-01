using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class administratort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 32, 28, 587, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 32, 28, 587, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 32, 28, 587, DateTimeKind.Utc).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 32, 28, 587, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b"), 0 },
                    { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b"), 0 },
                    { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b"), 0 },
                    { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("4c0a9494-51cc-438a-a47f-7ace8917fd9b") });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010));
        }
    }
}
