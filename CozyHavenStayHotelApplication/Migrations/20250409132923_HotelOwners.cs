using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyHavenStayHotelApplication.Migrations
{
    /// <inheritdoc />
    public partial class HotelOwners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountDetails",
                table: "HotelOwners");

            migrationBuilder.DropColumn(
                name: "TaxIdentification",
                table: "HotelOwners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountDetails",
                table: "HotelOwners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxIdentification",
                table: "HotelOwners",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
