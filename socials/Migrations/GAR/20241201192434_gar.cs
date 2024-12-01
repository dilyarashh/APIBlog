using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socials.Migrations.GAR
{
    /// <inheritdoc />
    public partial class gar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fias");

            migrationBuilder.CreateTable(
                name: "as_addr_obj",
                schema: "fias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    typename = table.Column<string>(type: "text", nullable: true),
                    level = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addr_Objs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_adm_hierarchy",
                schema: "fias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    parentobjid = table.Column<long>(type: "bigint", nullable: true),
                    isactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adm_Hier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_houses",
                schema: "fias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    objectid = table.Column<long>(type: "bigint", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    housenum = table.Column<string>(type: "text", nullable: true),
                    addnum1 = table.Column<string>(type: "text", nullable: true),
                    addnum2 = table.Column<string>(type: "text", nullable: true),
                    addtype1 = table.Column<int>(type: "integer", nullable: true),
                    addtype2 = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_obj",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "as_adm_hierarchy",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "as_houses",
                schema: "fias");
        }
    }
}
