using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.WebApi.Migrations
{
    public partial class AddProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bda30051-c030-467c-93e8-ae3e0b5bee4e",
                column: "ConcurrencyStamp",
                value: "a4fdf3ee-6253-41c4-af23-d024ad0c6666");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e15c12c3-5582-4711-9306-984e0df1dcdd",
                column: "ConcurrencyStamp",
                value: "39c6b541-0ff4-4e51-8503-b1668e4f0c01");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2314094f-0974-4783-ae24-97b801faf01d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "322bc160-2c16-4a17-a947-d0b96274f39f", "AQAAAAEAACcQAAAAEFtC7IW/TQaR8A8aUhrc43HHTBWwcSVNv8DfhtaWG1wYc7r2V867kTQCt7KUTC/jxg==", "a98fab7a-39e2-4aa6-a62e-f3e8e8f8d04e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1cb0e701-0a5e-4918-ac8a-2bdf7b4b69c7", "AQAAAAEAACcQAAAAECFRMEhAsjW7Zkfdr5Yr6OfDUvi4spThtZnR4SRlSPzR8F11aC3mFKe6xgfqPCMzmg==", "9d0ab209-d62a-44ba-8300-8cc31add0fd9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bda30051-c030-467c-93e8-ae3e0b5bee4e",
                column: "ConcurrencyStamp",
                value: "5c5b0b44-d94f-4c4b-938e-00a26f9bfe80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e15c12c3-5582-4711-9306-984e0df1dcdd",
                column: "ConcurrencyStamp",
                value: "58f75b4b-0b14-487c-9914-a2d212e8b675");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2314094f-0974-4783-ae24-97b801faf01d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "944b666f-5794-456a-9e92-88edc7efdd61", "AQAAAAEAACcQAAAAEKtOUESJidM5kbXiEcWBBr1knfieArxWDKYWZ5nyyTPhDJlulGefm0vBD4cwL4TY7g==", "eaccb38a-5a5f-4623-bcf8-7bd66fae8a60" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3305e55-491d-4629-8a5f-0f148c070ded", "AQAAAAEAACcQAAAAEEIPInJS1B5dwH+JwTdfJEgDChLZve+pdP7mZmbMYs/s2uLsiv/DfWjKLzl2gckL5Q==", "5cd241f2-ffba-477b-9cc4-9c326fdacd83" });
        }
    }
}
