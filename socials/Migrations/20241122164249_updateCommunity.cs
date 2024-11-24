using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class updateCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"));

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreateTime", "Description", "IsClosed", "Name", "SubscribersCount" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new DateTime(2024, 11, 22, 16, 42, 49, 43, DateTimeKind.Utc).AddTicks(4070), "Публикуем мемы с котами!", false, "Котята", 1 },
                    { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new DateTime(2024, 11, 22, 16, 42, 49, 43, DateTimeKind.Utc).AddTicks(4080), "Делимся мнением о прочитанных книгах", false, "Книжный клуб", 1 },
                    { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new DateTime(2024, 11, 22, 16, 42, 49, 43, DateTimeKind.Utc).AddTicks(4080), "Самая модная одежда", false, "Самый крутой магазин одежды", 1 },
                    { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new DateTime(2024, 11, 22, 16, 42, 49, 43, DateTimeKind.Utc).AddTicks(4080), "Одобряем заявку только избранным", true, "Секретное сообщество", 1 }
                });

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 },
                    { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 },
                    { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0"), 1 },
                    { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0") });

            migrationBuilder.DeleteData(
                table: "CommunityUsers",
                keyColumns: new[] { "CommunityId", "UserId" },
                keyValues: new object[] { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c") });

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"));

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreateTime", "Description", "IsClosed", "Name", "SubscribersCount" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580), "Публикуем мемы с котами!", false, "Котята", 2 },
                    { new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"), new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580), "Самая модная одежда", false, "Магазин одежды", 1 },
                    { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580), "Делимся мнением о прочитанных книгах", false, "Книжный клуб", 1 },
                    { new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"), new DateTime(2024, 11, 22, 16, 38, 55, 49, DateTimeKind.Utc).AddTicks(580), "Одобряем заявку только избранным", true, "Секретное сообщество", 1 }
                });

            migrationBuilder.InsertData(
                table: "CommunityUsers",
                columns: new[] { "CommunityId", "UserId", "Role" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 },
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0"), 1 },
                    { new Guid("f0e6d8c9-b6a5-1234-9876-543110fedcba"), new Guid("64a8ff7a-537f-48f7-8351-f7cdefa89ff0"), 0 },
                    { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 },
                    { new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"), new Guid("1a85e616-8ff4-4a27-8859-14b444939b6c"), 0 }
                });
        }
    }
}
