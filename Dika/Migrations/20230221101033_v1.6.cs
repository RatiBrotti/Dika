using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dika.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Barcode",
                table: "Inventory",
                column: "Barcode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventory_Barcode",
                table: "Inventory");
        }
    }
}
