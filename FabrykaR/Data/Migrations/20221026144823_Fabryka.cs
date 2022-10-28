using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrykaR.Data.Migrations
{
    public partial class Fabryka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HalaSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalaSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatorSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placa = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaszynaSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_uruchomienia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HalaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaszynaSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaszynaSet_HalaSet_HalaId",
                        column: x => x.HalaId,
                        principalTable: "HalaSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperatorMaszynaSet",
                columns: table => new
                {
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    MaszynaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorMaszynaSet", x => new { x.MaszynaId, x.OperatorId });
                    table.ForeignKey(
                        name: "FK_OperatorMaszynaSet_MaszynaSet_MaszynaId",
                        column: x => x.MaszynaId,
                        principalTable: "MaszynaSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperatorMaszynaSet_OperatorSet_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "OperatorSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaszynaSet_HalaId",
                table: "MaszynaSet",
                column: "HalaId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorMaszynaSet_OperatorId",
                table: "OperatorMaszynaSet",
                column: "OperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperatorMaszynaSet");

            migrationBuilder.DropTable(
                name: "MaszynaSet");

            migrationBuilder.DropTable(
                name: "OperatorSet");

            migrationBuilder.DropTable(
                name: "HalaSet");
        }
    }
}
