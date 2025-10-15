using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "J.K.", "Rowling" },
                    { 2, "George", "Orwell" },
                    { 3, "Harper", "Lee" },
                    { 4, "F. Scott", "Fitzgerald" },
                    { 5, "Jane", "Austen" },
                    { 6, "Mark", "Twain" },
                    { 7, "Ernest", "Hemingway" },
                    { 8, "Agatha", "Christie" },
                    { 9, "Stephen", "King" },
                    { 10, "Toni", "Morrison" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Science Fiction" },
                    { 3, "Mystery" },
                    { 4, "Romance" },
                    { 5, "Thriller" },
                    { 6, "Horror" },
                    { 7, "Historical Fiction" },
                    { 8, "Literary Fiction" },
                    { 9, "Adventure" },
                    { 10, "Classic" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bloomsbury Publishing Plc" },
                    { 2, "Harper & Brothers" },
                    { 3, "Canongate Books" },
                    { 4, "Ian Allan Publishing" },
                    { 5, "Bison Books" },
                    { 6, "Ladybird Books" },
                    { 7, "Harlequin Enterprises Ltd" },
                    { 8, "Medknow Publications" },
                    { 9, "University of California Press" },
                    { 10, "Emerald Group Publishing" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password" },
                values: new object[,]
                {
                    { 1, "james.miller92@gmail.com", false, "pbkdf2_sha256$100000$2133ad0a4d31462ef5766ff6b03c7987$f919d3c6f899fe09d8f3158618e63988a1d09daea26e0aeed5219768f1e2d199" },
                    { 2, "sarah.lee@outlook.com", false, "pbkdf2_sha256$100000$0acfd8d2ff40624052f26f2fb3495254$93857a9a6b61f8f9098e639f645f7143d92006ce925fc50e7af534567cafe07f" },
                    { 3, "marco.rossi89@yahoo.com", false, "pbkdf2_sha256$100000$6190de7b0dda9c8235e0e55455d43f2b$f33f0dfb4085e26e30dd79b9c9959a0a01a033155b4cfe8b9e8b9baa9670b8c3" },
                    { 4, "emily_watson@icloud.com", false, "pbkdf2_sha256$100000$84beb8171db67abbd93e38551e232120$d47e1668e7ced2fa8462c530ea3f8081e118c4eeb69ba9f7d640c2f6e860fae7" },
                    { 5, "david.nguyen@protonmail.com", false, "pbkdf2_sha256$100000$6a9df2a924d667a1bd472317344c8f26$45a2a5bc1f82bb6a1c61c33e434fd1b0c0e66cadb6b1bb4d4bffa89bd2687a3b" },
                    { 6, "lucas-johnson@live.com", false, "pbkdf2_sha256$100000$f67ad2c96b570f5e8c7def3da9f9cc7b$75b5631e544d43972e646d51ab14479771044310b42d0901cb4e2a067192e4fa" },
                    { 7, "maria.garcia23@bookhub.com", true, "pbkdf2_sha256$100000$6973359e189c00dff9c42cc516cca724$17ae99a40f51d065b02ad1ded4959166e67499c1c6f1fe7a1dea1036b3530dd6" },
                    { 8, "andrew.smith@bookhub.com", true, "pbkdf2_sha256$100000$463d7f87b63f4843bd6b899bced0acdd$cb8c5551c30f874fb846de9c7cb56595e12baa83c346ad6950504c500519c002" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "Price", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, "A young wizard's journey begins at Hogwarts School of Witchcraft and Wizardry.", 19.99m, 1, "Harry Potter and the Philosopher's Stone" },
                    { 2, "A dystopian social science fiction novel about totalitarian surveillance.", 15.99m, 2, "1984" },
                    { 3, "A novel about racial injustice and childhood in the American South.", 14.99m, 2, "To Kill a Mockingbird" },
                    { 4, "A critique of the American Dream set in the Jazz Age.", 12.99m, 2, "The Great Gatsby" },
                    { 5, "A romantic novel about manners, upbringing, morality, and marriage.", 13.99m, 3, "Pride and Prejudice" },
                    { 6, "A boy's adventures along the Mississippi River.", 11.99m, 2, "The Adventures of Tom Sawyer" },
                    { 7, "An aging Cuban fisherman's struggle with a giant marlin.", 10.99m, 2, "The Old Man and the Sea" },
                    { 8, "A detective story featuring Hercule Poirot on a luxury train.", 16.99m, 3, "Murder on the Orient Express" },
                    { 9, "A horror novel about a family isolated in a haunted hotel.", 18.99m, 4, "The Shining" },
                    { 10, "A story about the legacy of slavery and trauma.", 17.99m, 5, "Beloved" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 8, 8, 8 },
                    { 9, 9, 9 },
                    { 10, 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "Id", "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 8 },
                    { 4, 4, 10 },
                    { 5, 5, 4 },
                    { 6, 6, 9 },
                    { 7, 7, 8 },
                    { 8, 8, 3 },
                    { 9, 9, 6 },
                    { 10, 10, 8 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "BookId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 3 },
                    { 5, 5, 4 },
                    { 6, 6, 5 },
                    { 7, 3, 6 },
                    { 8, 4, 6 },
                    { 9, 7, 6 },
                    { 10, 8, 6 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "BookId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "I kept waiting for the story to pick up, but it never really went anywhere.", 1, 1, 1 },
                    { 2, "The premise sounded great, but the execution was flat and predictable.", 2, 2, 1 },
                    { 3, "A fast-paced read that kept me hooked, though the ending felt a bit rushed.", 3, 3, 2 },
                    { 4, "Beautiful writing, but I struggled with the slow middle chapters.", 4, 4, 3 },
                    { 5, "Loved the characters — they felt real and flawed in the best way.", 4, 5, 4 },
                    { 6, "The characters felt one-dimensional and I couldn’t connect with any of them.", 2, 1, 4 },
                    { 7, "Too many clichés and not enough originality — I lost interest halfway through.", 7, 2, 4 },
                    { 8, "A solid story, though I guessed the twist halfway through.", 8, 3, 6 },
                    { 9, "Enjoyable overall, but I didn’t connect with the main character.", 3, 4, 8 },
                    { 10, "An emotional rollercoaster — I cried more than once.", 1, 5, 8 }
                });

            migrationBuilder.InsertData(
                table: "WishlistItems",
                columns: new[] { "Id", "BookId", "UserId" },
                values: new object[,]
                {
                    { 1, 7, 3 },
                    { 2, 6, 3 },
                    { 3, 5, 1 },
                    { 4, 4, 5 },
                    { 5, 3, 4 },
                    { 6, 2, 7 },
                    { 7, 7, 8 },
                    { 8, 6, 8 },
                    { 9, 2, 8 },
                    { 10, 1, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_UserId",
                table: "OrderItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_BookId",
                table: "WishlistItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId",
                table: "WishlistItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
