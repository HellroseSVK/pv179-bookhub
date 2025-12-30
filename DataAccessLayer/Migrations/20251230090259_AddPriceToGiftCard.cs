using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToGiftCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "GiftCards",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 150m);

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "Title" },
                values: new object[] { 200m, "Welcome Gift Card" });

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "Title" },
                values: new object[] { 50m, "Seasonal Surprise Gift Card" });

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "Title" },
                values: new object[] { 200m, "Care Package Gift Card" });

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "Title" },
                values: new object[] { 250m, "Thinking of You Gift Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "GiftCards");

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Thinking of You Gift Card");

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Welcome Gift Card");

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "Get Well Soon Gift Card");

            migrationBuilder.UpdateData(
                table: "GiftCards",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "Seasonal Surprise Gift Card");
        }
    }
}
