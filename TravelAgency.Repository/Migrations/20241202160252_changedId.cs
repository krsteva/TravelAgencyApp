using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItineraryInPackage_Itineraries_ItineraryID",
                table: "ItineraryInPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_ItineraryInPackage_TravelPackages_TravelPackageId",
                table: "ItineraryInPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItineraryInPackage",
                table: "ItineraryInPackage");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "ItineraryInPackage");

            migrationBuilder.RenameTable(
                name: "ItineraryInPackage",
                newName: "ItineraryInPackages");

            migrationBuilder.RenameColumn(
                name: "ItineraryID",
                table: "ItineraryInPackages",
                newName: "ItineraryId");

            migrationBuilder.RenameIndex(
                name: "IX_ItineraryInPackage_TravelPackageId",
                table: "ItineraryInPackages",
                newName: "IX_ItineraryInPackages_TravelPackageId");

            migrationBuilder.RenameIndex(
                name: "IX_ItineraryInPackage_ItineraryID",
                table: "ItineraryInPackages",
                newName: "IX_ItineraryInPackages_ItineraryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItineraryInPackages",
                table: "ItineraryInPackages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItineraryInPackages_Itineraries_ItineraryId",
                table: "ItineraryInPackages",
                column: "ItineraryId",
                principalTable: "Itineraries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItineraryInPackages_TravelPackages_TravelPackageId",
                table: "ItineraryInPackages",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItineraryInPackages_Itineraries_ItineraryId",
                table: "ItineraryInPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_ItineraryInPackages_TravelPackages_TravelPackageId",
                table: "ItineraryInPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItineraryInPackages",
                table: "ItineraryInPackages");

            migrationBuilder.RenameTable(
                name: "ItineraryInPackages",
                newName: "ItineraryInPackage");

            migrationBuilder.RenameColumn(
                name: "ItineraryId",
                table: "ItineraryInPackage",
                newName: "ItineraryID");

            migrationBuilder.RenameIndex(
                name: "IX_ItineraryInPackages_TravelPackageId",
                table: "ItineraryInPackage",
                newName: "IX_ItineraryInPackage_TravelPackageId");

            migrationBuilder.RenameIndex(
                name: "IX_ItineraryInPackages_ItineraryId",
                table: "ItineraryInPackage",
                newName: "IX_ItineraryInPackage_ItineraryID");

            migrationBuilder.AddColumn<Guid>(
                name: "PackageId",
                table: "ItineraryInPackage",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItineraryInPackage",
                table: "ItineraryInPackage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItineraryInPackage_Itineraries_ItineraryID",
                table: "ItineraryInPackage",
                column: "ItineraryID",
                principalTable: "Itineraries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItineraryInPackage_TravelPackages_TravelPackageId",
                table: "ItineraryInPackage",
                column: "TravelPackageId",
                principalTable: "TravelPackages",
                principalColumn: "Id");
        }
    }
}
