﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeBitByte.IDP.Migrations.ConfigurationDb
{
    public partial class Update_DuendeIdentityServer_v6_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DPoPClockSkew",
                table: "Clients",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "DPoPValidationMode",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InitiateLoginUri",
                table: "Clients",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequireDPoP",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DPoPClockSkew",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DPoPValidationMode",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "InitiateLoginUri",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RequireDPoP",
                table: "Clients");
        }
    }
}
