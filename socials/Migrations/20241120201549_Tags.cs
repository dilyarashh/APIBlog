using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class Tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("2c4b19f5-511d-4f27-a914-08dbffad6d0e"), new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3256), "Новости" },
                    { new Guid("302d5c0c-5623-4810-a913-08dbffad6d0e"), new DateTime(2024, 11, 10, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3261), "Стажировка" },
                    { new Guid("4676b2f4-de54-4fce-a915-08dbffad6d0e"), new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3252), "Праздники" },
                    { new Guid("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"), new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3212), "Музыка" },
                    { new Guid("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"), new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3203), "Танцы" },
                    { new Guid("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"), new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3237), "Книги" },
                    { new Guid("75735935-74d3-4fa2-a918-08dbffad6d0e"), new DateTime(2024, 11, 12, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3232), "Фотография" },
                    { new Guid("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"), new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3217), "Кулинария" },
                    { new Guid("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"), new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3183), "Работа" },
                    { new Guid("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"), new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3247), "Рестораны" },
                    { new Guid("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"), new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3188), "Университет" },
                    { new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"), new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178), "Прогулка" },
                    { new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"), new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3198), "Рисование" },
                    { new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"), new DateTime(2024, 11, 10, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3266), "Путешествия" },
                    { new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"), new DateTime(2024, 11, 12, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3227), "Эстетика" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
