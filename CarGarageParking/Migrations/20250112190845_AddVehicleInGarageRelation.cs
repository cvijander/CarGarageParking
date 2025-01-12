using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGarageParking.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleInGarageRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInGarages_Owners_OwnerId",
                table: "VehicleInGarages",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId");
        }
    }
}
