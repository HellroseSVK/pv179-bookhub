using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                values: new object[] { 1, 2, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GenreId", "IsPrimary" },
                values: new object[] { 4, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 1, 9, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 2, 3, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 2, 4, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 2, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 2, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 3, 3, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 3, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 4, 4, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 5, 8, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 6, 5, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 7, 2, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 7, 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 7, 10, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 8, 3, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 9, 8, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 10, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 11, 5, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 11, 6, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "GenreId", "IsPrimary" },
                values: new object[] { 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 12, 4, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 13, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 13, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 13, 7, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 14, 9, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 15, 8, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 16, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 17, 1, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 17, 10, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 18, 8, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 19, 5, false });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 19, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 20, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 21, 1, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 22, 4, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "BookId", "IsPrimary" },
                values: new object[] { 23, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 24, 6, true });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "BookId", "GenreId", "IsPrimary" },
                values: new object[] { 24, 9, false });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "Id", "BookId", "GenreId", "IsPrimary" },
                values: new object[,]
                {
                    { 41, 25, 2, false },
                    { 42, 25, 3, true }
                });

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

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 42);

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
                column: "GenreId",
                value: 2);

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
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 18, 8 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 7,
                column: "BookId",
                value: 15);

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
                column: "BookId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 24, 6 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 24, 9 });

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
                column: "BookId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 8, 3 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 24, 6 });

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
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 19, 8 });

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
                column: "GenreId",
                value: 5);

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
                column: "BookId",
                value: 11);

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
                column: "BookId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 35,
                column: "BookId",
                value: 2);

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
                column: "BookId",
                value: 25);

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
