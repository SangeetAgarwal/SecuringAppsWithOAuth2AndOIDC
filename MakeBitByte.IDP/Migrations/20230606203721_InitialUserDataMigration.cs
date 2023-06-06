using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeBitByte.IDP.Migrations
{
    public partial class InitialUserDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SecurityCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SecurityCodeExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Email", "Password", "SecurityCode", "SecurityCodeExpiration", "Subject", "UserName" },
                values: new object[] { new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"), true, "9e373e3b-2643-40d2-a9c5-6383c14a3251", "app@anemail.com", "AQAAAAEAACcQAAAAEAz2SA93l305Govh36XNZX7QLHAYUfL1bGTgO+2sSwOgKwSkcQiUD9ir4fG8U4E1GA==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "da0d5f9a-8217-4a68-8398-db8506fdf618", "Appa" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Email", "Password", "SecurityCode", "SecurityCodeExpiration", "Subject", "UserName" },
                values: new object[] { new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"), true, "42d05044-8a36-40bd-b071-0e63ca762b32", "arjun@anemail.com", "AQAAAAEAACcQAAAAEAz2SA93l305Govh36XNZX7QLHAYUfL1bGTgO+2sSwOgKwSkcQiUD9ir4fG8U4E1GA==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1c94f897-2af3-46d4-a131-8a147d18a2f2", "Arjun" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Email", "Password", "SecurityCode", "SecurityCodeExpiration", "Subject", "UserName" },
                values: new object[] { new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"), true, "d57a4887-a497-4832-ac23-fc20d3da98d0", "vinita@anemail.com", "AQAAAAEAACcQAAAAEAz2SA93l305Govh36XNZX7QLHAYUfL1bGTgO+2sSwOgKwSkcQiUD9ir4fG8U4E1GA==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "36c434ff-888f-4820-a316-1a025f8a4f0c", "Vinita" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("0bda062a-4f41-4148-923d-d5ee352a6e45"), "b7efa697-96be-4372-bc85-4ed942e29323", "role", new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"), "none" },
                    { new Guid("0c6c1dc7-1ea3-45ee-97e3-b88febb1809a"), "5432b911-4f2c-4e8b-a938-30d520c682aa", "subscriberSince", new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"), "\"2023-01-21T00:00:00\"" },
                    { new Guid("214e0e21-a39e-4d83-9b33-ecdbd5eae4e8"), "4ce2b544-520c-4a25-a263-453838260970", "family_name", new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"), "Agarwal" },
                    { new Guid("3651fd20-11cf-4417-b0ad-423d43e7b18f"), "734aa10b-3770-407b-927b-b4858d83ce44", "role", new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"), "pro" },
                    { new Guid("6e6cdb7a-4aaa-438a-9618-bbd1e6d6113e"), "7497af29-05df-40e3-a1bc-574f5dd80d20", "given_name", new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"), "Appa" },
                    { new Guid("89e3cc7e-59f6-4ba6-9dfc-4020ddb76ebb"), "9e46806d-4864-4b71-8369-857f9934a193", "given_name", new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"), "Arjun" },
                    { new Guid("9510223d-d440-48c0-b3bf-4925cef028c0"), "15cc9230-490e-4286-ba79-a4562769ee02", "family_name", new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"), "Agarwal" },
                    { new Guid("a3f8a5fc-841c-47b1-8f1c-dd366f67b359"), "e2a24490-3c62-4d18-b4fe-369c9552b85a", "subscriberSince", new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"), "\"2021-06-21T00:00:00\"" },
                    { new Guid("b180d46d-3fbc-4601-86fc-e069dea1038d"), "e1281bff-a682-4ad7-9772-4a0f0e42584d", "role", new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"), "pro" },
                    { new Guid("b649b2d1-3e21-45b1-9615-dcc0e28f8993"), "96330052-b8b9-4291-8c92-a4667460b4b8", "family_name", new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"), "Agarwal" },
                    { new Guid("d9f335ef-dce4-4d48-a020-f2e9e4b08af7"), "dcde57a7-146b-4d85-b151-d94fc1195915", "subscriberSince", new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"), "\"9999-12-31T23:59:59.9999999\"" },
                    { new Guid("ff977f00-7b8d-4460-987c-bf5901fc2c9c"), "a68e5437-b932-44a2-a7b0-85f59f3a66c6", "given_name", new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"), "Vinita" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
