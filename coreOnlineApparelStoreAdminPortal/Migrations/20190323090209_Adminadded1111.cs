using Microsoft.EntityFrameworkCore.Migrations;

namespace coreOnlineApparelStoreAdminPortal.Migrations
{
    public partial class Adminadded1111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerZipNumber",
                table: "Admins",
                newName: "AdminZipNumber");

            migrationBuilder.RenameColumn(
                name: "CustomerState",
                table: "Admins",
                newName: "AdminState");

            migrationBuilder.RenameColumn(
                name: "CustomerPassword",
                table: "Admins",
                newName: "AdminPassword");

            migrationBuilder.RenameColumn(
                name: "CustomerGender",
                table: "Admins",
                newName: "AdminGender");

            migrationBuilder.RenameColumn(
                name: "CustomerCountry",
                table: "Admins",
                newName: "AdminCountry");

            migrationBuilder.RenameColumn(
                name: "CustomerAddress1",
                table: "Admins",
                newName: "AdminAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdminZipNumber",
                table: "Admins",
                newName: "CustomerZipNumber");

            migrationBuilder.RenameColumn(
                name: "AdminState",
                table: "Admins",
                newName: "CustomerState");

            migrationBuilder.RenameColumn(
                name: "AdminPassword",
                table: "Admins",
                newName: "CustomerPassword");

            migrationBuilder.RenameColumn(
                name: "AdminGender",
                table: "Admins",
                newName: "CustomerGender");

            migrationBuilder.RenameColumn(
                name: "AdminCountry",
                table: "Admins",
                newName: "CustomerCountry");

            migrationBuilder.RenameColumn(
                name: "AdminAddress",
                table: "Admins",
                newName: "CustomerAddress1");
        }
    }
}
