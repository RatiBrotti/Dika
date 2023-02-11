using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dika.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ContactIDSequence",
                startValue: 0L,
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "D",
                startValue: 0L);

            migrationBuilder.CreateSequence(
                name: "val");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityOfProvider = table.Column<int>(type: "int", nullable: false),
                    QuantityCounted = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropSequence(
                name: "ContactIDSequence");

            migrationBuilder.DropSequence(
                name: "D");

            migrationBuilder.DropSequence(
                name: "val");
        }
    }
}
