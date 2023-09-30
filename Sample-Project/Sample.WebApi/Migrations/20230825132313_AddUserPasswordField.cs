using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.WebApi.Migrations
{
    public partial class AddUserPasswordField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bda30051-c030-467c-93e8-ae3e0b5bee4e",
                column: "ConcurrencyStamp",
                value: "8e6799bb-60b3-4c12-94b9-f545fc52a444");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e15c12c3-5582-4711-9306-984e0df1dcdd",
                column: "ConcurrencyStamp",
                value: "41f78b37-17cc-4456-aae8-5916a8f35cb8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2314094f-0974-4783-ae24-97b801faf01d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6aadbc95-9026-4242-a88d-5f34f67b9d95", "AQAAAAEAACcQAAAAEK6xtmFX8/MsQ5/0qSHeCsBQ8Y3wqYfLytL9lZI5tdVp7JIP8QbIaznLU1O7FX23qA==", "f01b449b-e3e6-45d3-9202-286d0a39d8c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "264a3f77-2fd0-4aa7-9463-7f0857d94a5e", "AQAAAAEAACcQAAAAEL73YsyU2mDNu1p7lwBflCIhRi9ZxUHFuLYiIywLT0DcoQ7/HPANxtzmC586qTE7NA==", "9993223e-c8b2-4c14-9f63-785be42518af" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPassword",
                table: "AspNetUsers");

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
    }
}
