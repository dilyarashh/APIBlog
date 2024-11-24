using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class newSubscriber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7760));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7770));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7770));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7760));

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("2b4d3b8b-f3ae-4b9a-9456-cec31003f7fa"), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("2b4d3b8b-f3ae-4b9a-9456-cec31003f7fa") });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 13, 15, 3, 404, DateTimeKind.Utc).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 13, 15, 3, 404, DateTimeKind.Utc).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 13, 15, 3, 404, DateTimeKind.Utc).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 13, 15, 3, 404, DateTimeKind.Utc).AddTicks(2740));
        }
    }
}
