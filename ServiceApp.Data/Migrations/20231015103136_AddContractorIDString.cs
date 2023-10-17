using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContractorIDString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Cities_CityID",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Suburbs_SuburbID",
                table: "Contractors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors");

            migrationBuilder.RenameTable(
                name: "Contractors",
                newName: "Contractors2");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_SuburbID",
                table: "Contractors2",
                newName: "IX_Contractors2_SuburbID");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors_CityID",
                table: "Contractors2",
                newName: "IX_Contractors2_CityID");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Contractors2",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractors2",
                table: "Contractors2",
                column: "ContractorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors2_Cities_CityID",
                table: "Contractors2",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors2_Suburbs_SuburbID",
                table: "Contractors2",
                column: "SuburbID",
                principalTable: "Suburbs",
                principalColumn: "SuburbID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contractors2_Cities_CityID",
                table: "Contractors2");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors2_Suburbs_SuburbID",
                table: "Contractors2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contractors2",
                table: "Contractors2");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Contractors2");

            migrationBuilder.RenameTable(
                name: "Contractors2",
                newName: "Contractors");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors2_SuburbID",
                table: "Contractors",
                newName: "IX_Contractors_SuburbID");

            migrationBuilder.RenameIndex(
                name: "IX_Contractors2_CityID",
                table: "Contractors",
                newName: "IX_Contractors_CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contractors",
                table: "Contractors",
                column: "ContractorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Cities_CityID",
                table: "Contractors",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Suburbs_SuburbID",
                table: "Contractors",
                column: "SuburbID",
                principalTable: "Suburbs",
                principalColumn: "SuburbID");
        }
    }
}
