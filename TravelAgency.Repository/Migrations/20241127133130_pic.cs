using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class pic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Itineraries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Itineraries");
        }
    }
}
