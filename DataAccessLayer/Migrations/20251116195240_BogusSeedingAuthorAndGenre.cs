using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BogusSeedingAuthorAndGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Jane", "Twain" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Toni", "Christie" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Ernest");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Harper", "Rowling" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Toni", "Orwell" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "F. Scott", "King" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Surname",
                value: "King");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "George");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Surname",
                value: "Fitzgerald");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "J.K.", "Rowling" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 11, "George", "Lee" },
                    { 12, "Stephen", "Morrison" },
                    { 13, "Mark", "Twain" },
                    { 14, "F. Scott", "Twain" },
                    { 15, "Toni", "Orwell" },
                    { 16, "George", "Christie" },
                    { 17, "George", "Austen" },
                    { 18, "Stephen", "Lee" },
                    { 19, "Harper", "Hemingway" },
                    { 20, "Toni", "Hemingway" }
                });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 12, 3 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 7, 20 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 7, 22 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 5,
                column: "BookId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 16, 18 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 15, 15 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 19, 7 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 11, 2 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 11, 24 });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 12, 8, 2 },
                    { 13, 6, 13 },
                    { 14, 5, 2 },
                    { 15, 6, 8 },
                    { 20, 8, 1 },
                    { 22, 9, 11 },
                    { 23, 10, 19 },
                    { 24, 5, 25 },
                    { 26, 9, 6 },
                    { 27, 2, 10 },
                    { 28, 6, 7 },
                    { 33, 3, 7 },
                    { 35, 8, 2 },
                    { 36, 7, 4 },
                    { 38, 3, 25 },
                    { 40, 1, 17 }
                });

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
                column: "BookId",
                value: 1);

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

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "Id", "BookId", "GenreId" },
                values: new object[,]
                {
                    { 11, 24, 9 },
                    { 12, 2, 4 },
                    { 13, 13, 3 },
                    { 14, 2, 3 },
                    { 15, 8, 3 },
                    { 16, 24, 6 },
                    { 17, 18, 8 },
                    { 18, 17, 10 },
                    { 19, 19, 8 },
                    { 20, 1, 4 },
                    { 21, 14, 9 },
                    { 22, 11, 5 },
                    { 23, 19, 5 },
                    { 24, 25, 3 },
                    { 25, 11, 6 },
                    { 26, 6, 5 },
                    { 27, 10, 1 },
                    { 28, 7, 3 },
                    { 29, 13, 7 },
                    { 30, 16, 6 },
                    { 31, 7, 7 },
                    { 32, 1, 9 },
                    { 33, 7, 2 },
                    { 34, 2, 8 },
                    { 35, 2, 4 },
                    { 36, 4, 4 },
                    { 37, 13, 6 },
                    { 38, 25, 2 },
                    { 39, 11, 7 },
                    { 40, 17, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Science Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Horror");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Non-Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Graphic Novel");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Horror");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Thriller");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Thriller");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Young Adult");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Dystopian");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Fantasy");

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 11, 17, 24 },
                    { 16, 12, 24 },
                    { 17, 16, 18 },
                    { 18, 19, 17 },
                    { 19, 15, 19 },
                    { 21, 17, 14 },
                    { 25, 11, 11 },
                    { 29, 13, 13 },
                    { 30, 11, 16 },
                    { 31, 13, 7 },
                    { 32, 17, 1 },
                    { 34, 16, 2 },
                    { 37, 12, 13 },
                    { 39, 14, 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "J.K.", "Rowling" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "George", "Orwell" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Harper");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "F. Scott", "Fitzgerald" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Jane", "Austen" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Mark", "Twain" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Surname",
                value: "Hemingway");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Agatha");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Surname",
                value: "King");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Toni", "Morrison" });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 4, 4 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 5,
                column: "BookId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 6, 6 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 7, 7 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 8, 8 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 9, 9 });

            migrationBuilder.UpdateData(
                table: "BookAuthors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 10, 10 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 3, 8 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 4, 10 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 5, 4 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 6, 9 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 7,
                column: "BookId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 8, 3 });

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 9,
                column: "BookId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookId", "GenreId" },
                values: new object[] { 10, 8 });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Fantasy");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Science Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Mystery");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Romance");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Thriller");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Horror");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Historical Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Literary Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Adventure");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Classic");
        }
    }
}
