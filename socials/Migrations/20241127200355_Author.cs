using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations
{
    /// <inheritdoc />
    public partial class Author : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdressObject",
                columns: table => new
                {
                    Objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Objectid = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Typename = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "text", nullable: false),
                    Isactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdressObject", x => x.Objectguid);
                });

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 20, 3, 55, 517, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 20, 3, 55, 517, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 20, 3, 55, 517, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 20, 3, 55, 517, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AddressId",
                table: "Posts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AdressObject_AddressId",
                table: "Posts",
                column: "AddressId",
                principalTable: "AdressObject",
                principalColumn: "Objectguid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AdressObject_AddressId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "AdressObject");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AddressId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 6, 26, 45, 344, DateTimeKind.Utc).AddTicks(2950));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 6, 26, 45, 344, DateTimeKind.Utc).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 6, 26, 45, 344, DateTimeKind.Utc).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Communities",
                keyColumn: "Id",
                keyValue: new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                column: "CreateTime",
                value: new DateTime(2024, 11, 27, 6, 26, 45, 344, DateTimeKind.Utc).AddTicks(2950));
        }
    }
}
