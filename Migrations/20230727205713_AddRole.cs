using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Project.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 27, 20, 57, 13, 307, DateTimeKind.Utc).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HashedPassword", "Role" },
                values: new object[] { "$2a$11$QPYrNG52dYOBpQXCpdTXQuKbuijxQtOII7FLZXueJilbB/MIBjQMS", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 27, 20, 21, 13, 920, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "$2a$11$/0xxM3Ock0nEsEyiJNrMseF9g0y58MtpQrUse3NEN0dd/fVjYpYw6");
        }
    }
}
