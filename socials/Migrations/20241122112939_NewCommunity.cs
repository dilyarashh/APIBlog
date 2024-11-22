using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class NewCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreateTime", "Description", "IsClosed", "Name", "SubscribersCount" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new DateTime(2024, 11, 22, 11, 29, 39, 769, DateTimeKind.Utc).AddTicks(3240), "Публикуем мемы с котами!", false, "Котята", 0 },
                    { new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"), new DateTime(2024, 11, 22, 11, 29, 39, 769, DateTimeKind.Utc).AddTicks(3250), "Делимся мнением о прочитанных книгах", false, "Книжный клуб", 0 },
                    { new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"), new DateTime(2024, 11, 22, 11, 29, 39, 769, DateTimeKind.Utc).AddTicks(3250), "Одобряем заявку только избранным", true, "Секретное сообщество", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-4321-9876-543110fedcba"));

            migrationBuilder.DeleteData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-4321-9876-543210fedcba"));
        }
    }
}
