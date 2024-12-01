using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Blacktoken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    SubscribersCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ReadingTime = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    CommunityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CommunityName = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    Likes = table.Column<int>(type: "integer", nullable: false),
                    HasLike = table.Column<bool>(type: "boolean", nullable: false),
                    CommentsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    SubComments = table.Column<int>(type: "integer", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityUsers",
                columns: table => new
                {
                    CommunityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityUsers", x => new { x.CommunityId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CommunityUsers_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Communities",
                columns: new[] { "Id", "CreateTime", "Description", "IsClosed", "Name", "SubscribersCount" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"), new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010), "Публикуем мемы с котами!", false, "Котята", 1 },
                    { new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"), new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010), "Делимся мнением о прочитанных книгах", false, "Книжный клуб", 1 },
                    { new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"), new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010), "Самая модная одежда", false, "Самый крутой магазин одежды", 1 },
                    { new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"), new DateTime(2024, 12, 1, 12, 45, 29, 39, DateTimeKind.Utc).AddTicks(3010), "Одобряем заявку только избранным", true, "Секретное сообщество", 1 }
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
                    { new Guid("d35c6890-d26d-450b-a920-08dbffad6d0e"), new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178), "Увлечения" },
                    { new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"), new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178), "Прогулка" },
                    { new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"), new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3198), "Рисование" },
                    { new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"), new DateTime(2024, 11, 10, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3266), "Путешествия" },
                    { new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"), new DateTime(2024, 11, 12, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3227), "Эстетика" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityUsers_UserId",
                table: "CommunityUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CommunityUsers");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
