using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class postLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostLikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 21, 47, 43, 985, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 21, 47, 43, 985, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 21, 47, 43, 985, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 21, 47, 43, 985, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId",
                table: "PostLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 50, 41, 458, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 50, 41, 458, DateTimeKind.Utc).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 50, 41, 458, DateTimeKind.Utc).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 12, 1, 16, 50, 41, 458, DateTimeKind.Utc).AddTicks(6540));
        }
    }
}
