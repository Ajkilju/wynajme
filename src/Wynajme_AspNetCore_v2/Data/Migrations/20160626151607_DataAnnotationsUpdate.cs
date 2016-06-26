using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wynajme_AspNetCore_v2.Data.Migrations
{
    public partial class DataAnnotationsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Zmywarka",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Wanna",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Tresc",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Prysznic",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Pralka",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Mikrofala",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Lodowka",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "KuchniaGaz",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "KuchniaEl",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Cena",
                table: "Ogloszenie",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Zmywarka",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Wanna",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tresc",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Prysznic",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Pralka",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Mikrofala",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Lodowka",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "KuchniaGaz",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "KuchniaEl",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cena",
                table: "Ogloszenie",
                nullable: true);
        }
    }
}
