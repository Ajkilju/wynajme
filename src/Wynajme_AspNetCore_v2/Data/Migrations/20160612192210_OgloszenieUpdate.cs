using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wynajme_AspNetCore_v2.Data.Migrations
{
    public partial class OgloszenieUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tresc",
                table: "Ogloszenie",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "DodatkoweWyposazenie",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KuchniaEl",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KuchniaGaz",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Lodowka",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Mikrofala",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Powierzchnia",
                table: "Ogloszenie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Pralka",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Prysznic",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Wanna",
                table: "Ogloszenie",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Zmywarka",
                table: "Ogloszenie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DodatkoweWyposazenie",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "KuchniaEl",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "KuchniaGaz",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Lodowka",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Mikrofala",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Powierzchnia",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Pralka",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Prysznic",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Wanna",
                table: "Ogloszenie");

            migrationBuilder.DropColumn(
                name: "Zmywarka",
                table: "Ogloszenie");

            migrationBuilder.AlterColumn<string>(
                name: "Tresc",
                table: "Ogloszenie",
                nullable: false);
        }
    }
}
