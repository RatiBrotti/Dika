using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dika.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Inventory");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Inventory",
                type: "Float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Author");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Author",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Id");
        }
    }
}
