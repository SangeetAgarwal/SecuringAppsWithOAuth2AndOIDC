using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Api.Datalayer.Migrations
{
    public partial class CreateNoteTableAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("1115c7d1-d7bd-432c-a469-c6030a5c141c"), "Ultrices sagittis orci a scelerisque purus semper. Maecenas sed enim ut sem viverra aliquet eget sit amet. Dui sapien eget mi proin sed libero. Donec et odio pellentesque diam volutpat commodo. Ultrices gravida dictum fusce ut. Diam vulputate ut pharetra sit amet aliquam id diam maecenas. Tristique sollicitudin nibh sit amet. ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Ultrices sagittis orci a scelerisque purus semper. ", null, "(A note by Vinita) Ultrices sagittis orci a scelerisque purus semper.", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("1817e743-574c-41de-9912-622685d77c52"), "Senectus et netus et malesuada fames ac turpis. Faucibus turpis in eu mi bibendum neque. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar. Vitae congue mauris rhoncus aenean vel elit scelerisque. ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Senectus et netus et malesuada fames ac turpis. ", null, "(A note by Vinita) Senectus et netus et malesuada fames ac turpis", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("41b3ae06-4e47-44d0-bb5d-ee5d0ad4f6c5"), "Id interdum velit laoreet id donec ultrices tincidunt arcu. Consequat nisl vel pretium lectus quam id. Tellus mauris a diam maecenas sed enim ut sem. Ullamcorper malesuada proin libero nunc consequat interdum. Nibh sit amet commodo nulla facilisi nullam. Tristique nulla aliquet enim tortor at auctor.", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Id interdum velit laoreet id donec ultrices tincidunt arcu. ", null, "(A note by Vinita) Id interdum velit laoreet id donec ultrices tincidunt arcu. ", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("4411613e-5e53-49f0-a198-74603ffdb524"), "In est ante in nibh mauris cursus. Massa tempor nec feugiat nisl pretium fusce id. Morbi quis commodo odio aenean sed adipiscing. Pretium viverra suspendisse potenti nullam ac tortor vitae purus. Placerat in egestas erat imperdiet.", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "In est ante in nibh mauris cursus. Massa tempor nec feugiat nisl pretium fusce id. ", null, "(A note by Arjun) In est ante in nibh mauris cursus", new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("54b1dbf7-596d-4d03-86fa-4097dbc9f8e5"), "Eu mi bibendum neque egestas congue quisque egestas. At quis risus sed vulputate odio. Nunc pulvinar sapien et ligula ullamcorper malesuada proin libero. Elit at imperdiet dui accumsan sit amet nulla facilisi morbi. Mauris pellentesque pulvinar pellentesque habitant morbi tristique senectus et. A diam maecenas sed enim.` ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Eu mi bibendum neque egestas congue quisque egestas. ", null, "(A note by Vinita) Eu mi bibendum neque egestas congue quisque egestas.", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("5ad62e22-590e-4586-b46c-47d64562f5e4"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Amet porttitor eget dolor morbi non. Varius vel pharetra vel turpis nunc eget. Augue ut lectus arcu bibendum at varius. Accumsan tortor posuere ac ut. In tellus integer feugiat scelerisque varius morbi enim nunc. Turpis massa sed elementum tempus egestas sed sed risus. Volutpat consequat mauris nunc congue. Mauris commodo quis imperdiet massa tincidunt nunc. Mattis rhoncus urna neque viverra. Arcu odio ut sem nulla pharetra diam sit amet. Sagittis nisl rhoncus mattis rhoncus urna neque viverra justo nec. Etiam tempor orci eu lobortis elementum nibh tellus. Cursus vitae congue mauris rhoncus aenean.", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Lorem ipsum dolor sit amet, consectetur adipiscing elit,", null, "(A note by Arjun) consectetur adipiscing elit", new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("72dd664a-a6c0-4bd4-874c-892b86b91859"), "Amet risus nullam eget felis eget nunc lobortis mattis aliquam. Consectetur adipiscing elit duis tristique sollicitudin nibh sit amet. Faucibus a pellentesque sit amet porttitor eget dolor morbi. Pellentesque habitant morbi tristique senectus et netus et. Quam viverra orci sagittis eu volutpat odio facilisis. Facilisi cras fermentum odio eu.  ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Amet risus nullam eget felis eget nunc lobortis mattis ", null, "(A note by Arjun) Amet risus nullam eget felis eget nunc lobortis mattis ", new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("77eede84-249e-49bd-963e-34ce882bb0ce"), "Nunc lobortis mattis aliquam faucibus purus in massa tempor. Ultricies leo integer malesuada nunc. Eget duis at tellus at urna. Aenean vel elit scelerisque mauris pellentesque pulvinar. Quisque id diam vel quam elementum pulvinar etiam non quam. Lectus urna duis convallis convallis tellus id interdum velit.  ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Nunc lobortis mattis aliquam faucibus purus in ", null, "(A note by Arjun) Erat pellentesque adipiscing commodo elit at imperdiet dui accumsan.", new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("891952fb-df7c-4493-95ca-f8d4628ecc85"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Amet porttitor eget dolor morbi non. Varius vel pharetra vel turpis nunc eget. Augue ut lectus arcu bibendum at varius. Accumsan tortor posuere ac ut. In tellus integer feugiat scelerisque varius morbi enim nunc. Turpis massa sed elementum tempus egestas sed sed risus. Volutpat consequat mauris nunc congue. Mauris commodo quis imperdiet massa tincidunt nunc. Mattis rhoncus urna neque viverra. Arcu odio ut sem nulla pharetra diam sit amet. Sagittis nisl rhoncus mattis rhoncus urna neque viverra justo nec. Etiam tempor orci eu lobortis elementum nibh tellus. Cursus vitae congue mauris rhoncus aenean.", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Lorem ipsum", null, "(A note by Arjun) consectetur adipiscing elit", new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("b07e32d8-caa5-419b-9c0c-e246a805193f"), "Faucibus a pellentesque sit amet. Adipiscing elit pellentesque habitant morbi tristique. Cras semper auctor neque vitae tempus. Facilisis magna etiam tempor orci eu lobortis elementum nibh. Mattis vulputate enim nulla aliquet porttitor lacus luctus accumsan. ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Faucibus a pellentesque sit amet. Adipiscing elit ", null, "(A note by Vinita) Faucibus a pellentesque sit amet. Adipiscing elit ", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("b67d9bed-fede-4c41-8ba8-9eca258a20d3"), " Senectus et netus et malesuada fames ac turpis. Faucibus turpis in eu mi bibendum neque. Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar. Vitae congue mauris rhoncus aenean vel elit scelerisque. ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, " Senectus et netus et malesuada fames ac turpis. ", null, "(A note by Vinita) Senectus et netus et malesuada fames ac turpis", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "DateCreated", "DeleteFlag", "Description", "LastModified", "Title", "UserId" },
                values: new object[] { new Guid("c20db987-b190-4179-b365-060953a4d0dd"), "Aenean sed adipiscing diam donec adipiscing tristique risus nec feugiat. In egestas erat imperdiet sed euismod nisi porta. Eros donec ac odio tempor orci dapibus ultrices. Amet nisl suscipit adipiscing bibendum est. Neque convallis a cras semper auctor neque. Ut venenatis tellus in metus vulputate eu scelerisque. ", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Aenean sed adipiscing diam donec adipiscing tristique risus nec feugiat. ", null, "(A note by Vinita) Aenean sed adipiscing diam donec adipiscing tristique risus nec feugiat", new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
