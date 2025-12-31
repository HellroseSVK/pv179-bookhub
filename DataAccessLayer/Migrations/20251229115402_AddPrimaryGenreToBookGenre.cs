using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryGenreToBookGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "BookGenres",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 1, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 2, 3, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 2, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 3, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 4, 2, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 5, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 6, 4, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 6, 5, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 7, 1, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 7, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 7, 10, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 8, 4, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 9, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 10, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 10, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 11, 4, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 11, 8, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 12, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 12, 10, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 13, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 14, 3, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 14, 8, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 15, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 15, 8, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 16, 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 16, 10, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 17, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 18, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 19, 1, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 19, 10, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 20, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 20, 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 21, 5, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 21, 10, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 22, 9, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 23, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 23, 8, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 24, 4, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 25, 6, true });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId_IsPrimary",
                table: "BookGenres",
                columns: new[] { "BookId", "IsPrimary" },
                unique: true,
                filter: "IsPrimary = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookGenres_BookId_IsPrimary",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "BookGenres");

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 3, 6 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 20, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 22, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 6,
                column: "BookId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 15, 8 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 7, 10 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 2, 6 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 10,
                column: "BookId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 11,
                column: "BookId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 2, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 13, 3 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 2, 3 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 15,
                column: "BookId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 16,
                column: "BookId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 18, 8 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 17, 10 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 19,
                column: "BookId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 1, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 14, 9 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 11, 5 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 19, 5 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 24,
                column: "BookId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 11, 6 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 6, 5 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 10, 1 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 7, 3 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 13, 7 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 16, 6 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 7, 7 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 1, 9 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 7, 2 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 2, 8 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 2, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 4, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 13, 6 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 25, 2 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 11, 7 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 17, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");
        }
    }
}
