using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class communityusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 12, 41, 638, DateTimeKind.Utc).AddTicks(4720));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 12, 41, 638, DateTimeKind.Utc).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 12, 41, 638, DateTimeKind.Utc).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 12, 41, 638, DateTimeKind.Utc).AddTicks(4730));

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd"), 0 },
                    { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd"), 0 },
                    { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd"), 0 },
                    { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("e71da460-f06b-406f-800a-d18712fa9edd") });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5150));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5150));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5150));
        }
    }
}
