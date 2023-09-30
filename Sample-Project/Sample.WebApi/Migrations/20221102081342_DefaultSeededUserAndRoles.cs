using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.WebApi.Migrations
{
    public partial class DefaultSeededUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bda30051-c030-467c-93e8-ae3e0b5bee4e", "5c5b0b44-d94f-4c4b-938e-00a26f9bfe80", "Administrator", "ADMINISTRATOR" },
                    { "e15c12c3-5582-4711-9306-984e0df1dcdd", "58f75b4b-0b14-487c-9914-a2d212e8b675", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2314094f-0974-4783-ae24-97b801faf01d", 0, "944b666f-5794-456a-9e92-88edc7efdd61", "admin@admin.com", false, "System", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEKtOUESJidM5kbXiEcWBBr1knfieArxWDKYWZ5nyyTPhDJlulGefm0vBD4cwL4TY7g==", null, false, "noimage.png", "eaccb38a-5a5f-4623-bcf8-7bd66fae8a60", false, "admin@admin.com" },
                    { "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1", 0, "a3305e55-491d-4629-8a5f-0f148c070ded", "user@user.com", false, "System", "User", false, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAEAACcQAAAAEEIPInJS1B5dwH+JwTdfJEgDChLZve+pdP7mZmbMYs/s2uLsiv/DfWjKLzl2gckL5Q==", null, false, "noimage.png", "5cd241f2-ffba-477b-9cc4-9c326fdacd83", false, "user@user.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bda30051-c030-467c-93e8-ae3e0b5bee4e", "2314094f-0974-4783-ae24-97b801faf01d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e15c12c3-5582-4711-9306-984e0df1dcdd", "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bda30051-c030-467c-93e8-ae3e0b5bee4e", "2314094f-0974-4783-ae24-97b801faf01d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e15c12c3-5582-4711-9306-984e0df1dcdd", "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bda30051-c030-467c-93e8-ae3e0b5bee4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e15c12c3-5582-4711-9306-984e0df1dcdd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2314094f-0974-4783-ae24-97b801faf01d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe750ed8-92fd-484e-a3fa-dc5f4b62c0d1");
        }
    }
}
