using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class updateSubscribers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 16, 29, 58, 563, DateTimeKind.Utc).AddTicks(6360), 2 });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 16, 29, 58, 563, DateTimeKind.Utc).AddTicks(6370), 1 });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 16, 29, 58, 563, DateTimeKind.Utc).AddTicks(6370), 1 });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 16, 29, 58, 563, DateTimeKind.Utc).AddTicks(6370), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7760), 0 });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7770), 0 });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7770), 0 });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"),
                columns: new[] { "CreateTime", "SubscribersCount" },
                values: new object[] { new DateTime(2024, 11, 22, 14, 20, 25, 589, DateTimeKind.Utc).AddTicks(7760), 0 });
        }
    }
}
