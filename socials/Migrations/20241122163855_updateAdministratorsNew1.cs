using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class updateAdministratorsNew1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[] { new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0"), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") });

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

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[] { new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0"), 0 });
        }
    }
}
