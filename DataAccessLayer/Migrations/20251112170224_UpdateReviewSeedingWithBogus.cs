using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewSeedingWithBogus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Body", "BookId", "UserId" },
                values: new object[] { "Very disappointing read. Too many clichés and not enough originality. Would not recommend.", 6, 7 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "The characters felt real and well-developed. ENGAGING book that I would definitely recommend.", 3, 5, 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Body", "BookId" },
                values: new object[] { "It was fine, but didn't leave a lasting impression. AVERAGE overall.", 1 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Body", "BookId", "Rating" },
                values: new object[] { "The characters felt real and well-developed. MASTERFUL book that I would definitely recommend.", 9, 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "Unfortunately slow. The plot was too predictable for my taste. Had some potential but didn't deliver.", 8, 2, 5 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "Really engaging book. The emotional depth really resonated with me. Just a few minor issues kept it from being perfect.", 4, 4, 2 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "Really brilliant book. Beautiful writing style that flows perfectly. Just a few minor issues kept it from being perfect.", 5, 4, 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "I couldn't put this book down! EXCELLENT book that I would definitely recommend.", 5, 5, 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Body", "Rating", "UserId" },
                values: new object[] { "Beautiful writing style that flows perfectly. BRILLIANT book that I would definitely recommend.", 5, 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "It was fine, but didn't leave a lasting impression. ADEQUATE overall.", 2, 3, 7 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "BookId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 11, "Really captivating book. Beautiful writing style that flows perfectly. Just a few minor issues kept it from being perfect.", 5, 4, 1 },
                    { 12, "Really engaging book. I couldn't put this book down! Just a few minor issues kept it from being perfect.", 10, 4, 2 },
                    { 13, "Very predictable read. The plot was too predictable for my taste. Would not recommend.", 1, 1, 7 },
                    { 14, "Very frustrating read. Too many clichés and not enough originality. Would not recommend.", 8, 1, 3 },
                    { 15, "This book exceeded all my expectations. EXCELLENT book that I would definitely recommend.", 4, 5, 6 },
                    { 16, "Really brilliant book. I couldn't put this book down! Just a few minor issues kept it from being perfect.", 1, 4, 3 },
                    { 17, "Good enough to finish but wouldn't recommend highly. FINE overall.", 6, 3, 5 },
                    { 18, "Very weak read. I struggled to get through this one. Would not recommend.", 5, 1, 6 },
                    { 19, "Very tedious read. I lost interest halfway through. Would not recommend.", 3, 1, 4 },
                    { 20, "A solid story, though not groundbreaking. AVERAGE overall.", 5, 3, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Body", "BookId", "UserId" },
                values: new object[] { "I kept waiting for the story to pick up, but it never really went anywhere.", 1, 1 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "The premise sounded great, but the execution was flat and predictable.", 2, 2, 1 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Body", "BookId" },
                values: new object[] { "A fast-paced read that kept me hooked, though the ending felt a bit rushed.", 3 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Body", "BookId", "Rating" },
                values: new object[] { "Beautiful writing, but I struggled with the slow middle chapters.", 4, 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "Loved the characters — they felt real and flawed in the best way.", 4, 5, 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "The characters felt one-dimensional and I couldn’t connect with any of them.", 2, 1, 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "Too many clichés and not enough originality — I lost interest halfway through.", 7, 2, 4 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "A solid story, though I guessed the twist halfway through.", 8, 3, 6 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Body", "Rating", "UserId" },
                values: new object[] { "Enjoyable overall, but I didn’t connect with the main character.", 4, 8 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Body", "BookId", "Rating", "UserId" },
                values: new object[] { "An emotional rollercoaster — I cried more than once.", 1, 5, 8 });
        }
    }
}
