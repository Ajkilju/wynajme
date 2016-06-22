using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Wynajme_AspNetCore_v2.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    KategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.KategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Miasto",
                columns: table => new
                {
                    MiastoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miasto", x => x.MiastoId);
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenie",
                columns: table => new
                {
                    OgloszenieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cena = table.Column<int>(nullable: true),
                    DataDodania = table.Column<DateTime>(nullable: false),
                    KategoriaId = table.Column<int>(nullable: false),
                    MiastoId = table.Column<int>(nullable: false),
                    Miniature = table.Column<byte[]>(nullable: true),
                    MiniatureMimeType = table.Column<string>(nullable: true),
                    Tresc = table.Column<string>(nullable: false),
                    Tytul = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenie", x => x.OgloszenieId);
                    table.ForeignKey(
                        name: "FK_Ogloszenie_Kategoria_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategoria",
                        principalColumn: "KategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogloszenie_Miasto_MiastoId",
                        column: x => x.MiastoId,
                        principalTable: "Miasto",
                        principalColumn: "MiastoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageContent = table.Column<byte[]>(nullable: true),
                    ImageMimeType = table.Column<string>(nullable: true),
                    OgloszenieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_Ogloszenie_OgloszenieId",
                        column: x => x.OgloszenieId,
                        principalTable: "Ogloszenie",
                        principalColumn: "OgloszenieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_OgloszenieId",
                table: "Image",
                column: "OgloszenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenie_KategoriaId",
                table: "Ogloszenie",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenie_MiastoId",
                table: "Ogloszenie",
                column: "MiastoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Ogloszenie");

            migrationBuilder.DropTable(
                name: "Kategoria");

            migrationBuilder.DropTable(
                name: "Miasto");
        }
    }
}
