using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecomove_back.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarpoolAnnouncements_Vehicle_VehicleId",
                table: "CarpoolAnnouncements");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalVehicles_Vehicle_VehicleId",
                table: "RentalVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Category_CategoryId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Models_ModelId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Motorizations_MotorizationId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Status_StatusId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_StatusId",
                table: "Vehicles",
                newName: "IX_Vehicles_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_MotorizationId",
                table: "Vehicles",
                newName: "IX_Vehicles_MotorizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_ModelId",
                table: "Vehicles",
                newName: "IX_Vehicles_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_CategoryId",
                table: "Vehicles",
                newName: "IX_Vehicles_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarpoolAnnouncements_Vehicles_VehicleId",
                table: "CarpoolAnnouncements",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalVehicles_Vehicles_VehicleId",
                table: "RentalVehicles",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Categories_CategoryId",
                table: "Vehicles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Models_ModelId",
                table: "Vehicles",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Motorizations_MotorizationId",
                table: "Vehicles",
                column: "MotorizationId",
                principalTable: "Motorizations",
                principalColumn: "MotorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Status_StatusId",
                table: "Vehicles",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarpoolAnnouncements_Vehicles_VehicleId",
                table: "CarpoolAnnouncements");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalVehicles_Vehicles_VehicleId",
                table: "RentalVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Categories_CategoryId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Models_ModelId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Motorizations_MotorizationId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Status_StatusId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_StatusId",
                table: "Vehicle",
                newName: "IX_Vehicle_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_MotorizationId",
                table: "Vehicle",
                newName: "IX_Vehicle_MotorizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicle",
                newName: "IX_Vehicle_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_CategoryId",
                table: "Vehicle",
                newName: "IX_Vehicle_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarpoolAnnouncements_Vehicle_VehicleId",
                table: "CarpoolAnnouncements",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalVehicles_Vehicle_VehicleId",
                table: "RentalVehicles",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Category_CategoryId",
                table: "Vehicle",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Models_ModelId",
                table: "Vehicle",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Motorizations_MotorizationId",
                table: "Vehicle",
                column: "MotorizationId",
                principalTable: "Motorizations",
                principalColumn: "MotorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Status_StatusId",
                table: "Vehicle",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }
    }
}
