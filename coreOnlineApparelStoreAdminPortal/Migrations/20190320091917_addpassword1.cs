using Microsoft.EntityFrameworkCore.Migrations;

namespace coreOnlineApparelStoreAdminPortal.Migrations
{
    public partial class addpassword1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCountry2",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerPhoneNumber2",
                table: "Customers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerState2",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerZipNumber2",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCountry2",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerPhoneNumber2",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerState2",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerZipNumber2",
                table: "Customers");
        }
    }
}
