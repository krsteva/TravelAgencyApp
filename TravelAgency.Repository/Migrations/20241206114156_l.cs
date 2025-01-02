using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class l : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackagesId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TravelPackagesId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TravelPackagesId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TravelPackageId",
                table: "Bookings",
                column: "TravelPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackageId",
                table: "Bookings",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackageId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TravelPackageId",
                table: "Bookings");

            migrationBuilder.AddColumn<Guid>(
                name: "TravelPackagesId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TravelPackagesId",
                table: "Bookings",
                column: "TravelPackagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackagesId",
                table: "Bookings",
                column: "TravelPackagesId",
                principalTable: "TravelPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
