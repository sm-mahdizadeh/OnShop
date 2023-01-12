using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnShop.DAL.Migrations
{
    public partial class storeCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ff58fae6-8c30-41d9-a369-128c1742a27d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0844e9b9-84eb-4663-95a6-8537d9518065");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 22, 10, 4, 3, 685, DateTimeKind.Local).AddTicks(5126), new DateTime(2021, 1, 22, 10, 4, 3, 685, DateTimeKind.Local).AddTicks(6098) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 22, 10, 4, 3, 686, DateTimeKind.Local).AddTicks(1775), new DateTime(2021, 1, 22, 10, 4, 3, 686, DateTimeKind.Local).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ModifiedDate", "PasswordHash", "RegisteredDate" },
                values: new object[] { "3374d943-703a-4d44-8f25-a930dfd75370", new DateTime(2021, 1, 22, 10, 4, 3, 654, DateTimeKind.Local).AddTicks(4777), "AQAAAAEAACcQAAAAEEPDEFfY8YyW4dyJp8OPhpLfbvKp+0g8UdYXXvkDzSWT44jrC2yAgBnxk6AU8nUWFg==", new DateTime(2021, 1, 22, 10, 4, 3, 650, DateTimeKind.Local).AddTicks(8354) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ModifiedDate", "PasswordHash", "RegisteredDate" },
                values: new object[] { "bcbf0d78-7485-4594-84f8-d80f09020918", new DateTime(2021, 1, 22, 10, 4, 3, 668, DateTimeKind.Local).AddTicks(6162), "AQAAAAEAACcQAAAAEElv59vHrF4DeV3VskJsd8DcQhOjQvS4e4OavVSYAq4hmkKz0AL1l2PVhUm18ZiqXA==", new DateTime(2021, 1, 22, 10, 4, 3, 668, DateTimeKind.Local).AddTicks(6096) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "ModifiedDate", "PasswordHash", "RegisteredDate" },
                values: new object[] { "e30509e6-e689-4a6f-942f-149a9700c7c7", new DateTime(2021, 1, 22, 10, 4, 3, 684, DateTimeKind.Local).AddTicks(8119), "AQAAAAEAACcQAAAAEP0Xgei0Toib60EJHWCSP461BJmebrqDlM8DTA5aRo7sBWOjEUMAxvB3y0wiRFHbpA==", new DateTime(2021, 1, 22, 10, 4, 3, 684, DateTimeKind.Local).AddTicks(8052) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Stores");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fda25e3e-6572-49d6-b1d8-e4dfb3c7dcd4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b37e9ce2-90b0-4472-93f8-4350592c4a8a");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 10, 15, 396, DateTimeKind.Local).AddTicks(1036), new DateTime(2021, 1, 17, 21, 10, 15, 396, DateTimeKind.Local).AddTicks(1929) });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 17, 21, 10, 15, 396, DateTimeKind.Local).AddTicks(7505), new DateTime(2021, 1, 17, 21, 10, 15, 396, DateTimeKind.Local).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ModifiedDate", "PasswordHash", "RegisteredDate" },
                values: new object[] { "458c1b48-8a68-4f47-aa9d-59f65906789d", new DateTime(2021, 1, 17, 21, 10, 15, 355, DateTimeKind.Local).AddTicks(4684), "AQAAAAEAACcQAAAAEFhJV0JD33MrHTd18FOUCJbcopKK1JlRZIYN2W3yVpbjbcPLl/Zxlq3aL5DbVDb/FQ==", new DateTime(2021, 1, 17, 21, 10, 15, 348, DateTimeKind.Local).AddTicks(5807) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ModifiedDate", "PasswordHash", "RegisteredDate" },
                values: new object[] { "384fc998-0baf-412b-b26a-7750ebcabab9", new DateTime(2021, 1, 17, 21, 10, 15, 374, DateTimeKind.Local).AddTicks(3204), "AQAAAAEAACcQAAAAELuXfmpFbL2PyRJnCrp465epY3g8DlX5TdcBmkbMZv5B92u0VT1/7vbqi476ULfD4Q==", new DateTime(2021, 1, 17, 21, 10, 15, 374, DateTimeKind.Local).AddTicks(3117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "ModifiedDate", "PasswordHash", "RegisteredDate" },
                values: new object[] { "2b421932-16ea-498d-9511-765bec1a8cd5", new DateTime(2021, 1, 17, 21, 10, 15, 395, DateTimeKind.Local).AddTicks(3787), "AQAAAAEAACcQAAAAEKIg/SQXfooiwECibCuCJVcvOIvLHgzK/PJ3ENiup0bxxztIpHrFp3Pzjt+5xCNYsQ==", new DateTime(2021, 1, 17, 21, 10, 15, 395, DateTimeKind.Local).AddTicks(3706) });
        }
    }
}
