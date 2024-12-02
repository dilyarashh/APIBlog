using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class Administrators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e"), 0 },
                    { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e"), 0 },
                    { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e"), 0 },
                    { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("c5c44d4f-b9a5-4c79-ad5c-92eb263d461e") });

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
    }
}
