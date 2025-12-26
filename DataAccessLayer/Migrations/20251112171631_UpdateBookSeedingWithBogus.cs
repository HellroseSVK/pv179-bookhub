using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookSeedingWithBogus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A captivating tale of mystery and intrigue, set against the backdrop of war. Aut atque qui id deleniti nihil quo fuga tempora veniam id omnis non est.", 12.74m, 3, "The Ancient Memory" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A captivating tale of family and tradition, set in a world of magic and wonder. Enim ipsa hic laborum eaque doloremque asperiores maiores.", 12.93m, 10, "The Golden Marcus" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A breathtaking story of family and tradition, unfolding in the shadows of ancient cities. In debitis sed sed eos perferendis cumque blanditiis a culpa iusto hic et distinctio.", 22.59m, 3, "Theodore's Memory" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "An unforgettable story following adventure and danger, set against the backdrop of war. Consequatur eaque velit et ea autem iste debitis alias dolores non minus molestias consequatur.", 21.52m, 4, "Beyond the Journey" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A masterful exploration of hope and redemption, in a time of great change. Sed ipsa pariatur a sunt delectus sunt adipisci soluta voluptas reiciendis.", 19.48m, 6, "Soul in Dublin" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "An epic journey through power and corruption, in a realm where nothing is as it seems. Architecto esse at soluta sunt vel officia necessitatibus explicabo.", 19.42m, 6, "Quest in London" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A beautiful narrative exploring hope and redemption, unfolding in the shadows of ancient cities. Dolorem atque et voluptas eaque deleniti fuga accusamus saepe veritatis autem non.", 9.20m, 1, "The Lost Evangeline" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A masterful exploration of loss and discovery, where legends come to life. Atque hic aspernatur ea aperiam quia voluptatem modi quidem.", 15.60m, 10, "Secrets of the Eternal Kingdom" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A captivating tale of adventure and danger, where legends come to life. Aperiam quia suscipit nihil ratione repellendus enim vel ut dolores velit non eaque magnam dolores.", 27.66m, 10, "Adrienne's Tale" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A masterful exploration of family and tradition, set in a world of magic and wonder. Quis harum explicabo dolore corporis tenetur necessitatibus quibusdam velit ea laborum cum non.", 14.18m, 10, "Dark Myth" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "Price", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 11, "A haunting story about friendship and loyalty, in a time of great change. Cumque ut dolorem voluptatem magnam velit eum odio.", 16.42m, 5, "Alexander's Mind" },
                    { 12, "A beautiful narrative exploring love and betrayal, taking place across vast kingdoms. Omnis ad voluptatem voluptates nisi temporibus ea voluptatem.", 13.90m, 10, "The Golden Isabella" },
                    { 13, "A gripping saga of friendship and loyalty, where legends come to life. Unde repellat aut quia quia temporibus fugiat ut dolorum reprehenderit tempore dignissimos ut ab.", 15.10m, 9, "The Eternal Journey" },
                    { 14, "A compelling tale that love and betrayal, unfolding in the shadows of ancient cities. Exercitationem doloribus eum nam placeat maxime dolores tempore ipsum eligendi illo.", 25.80m, 2, "The Last Journey" },
                    { 15, "A masterful exploration of hope and redemption, spanning generations of conflict. Vel id vitae quia odit enim delectus quidem.", 23.76m, 10, "Secrets of the Hidden Mind" },
                    { 16, "An unforgettable story following family and tradition, in a time of great change. Est et ea ut dicta maxime dolores earum enim.", 23.88m, 1, "The Last Journey" },
                    { 17, "An unforgettable story following hope and redemption, in a time of great change. Modi nihil provident doloribus aut aut quis repudiandae quia inventore et sint fugit.", 17.04m, 8, "The Quest of Rome" },
                    { 18, "An unforgettable story following friendship and loyalty, in a time of great change. Facilis distinctio ut quo dolorem amet aut sit et consequatur tenetur et ut quis dolores.", 11.32m, 4, "The Mind of Copenhagen" },
                    { 19, "An epic journey through mystery and intrigue, unfolding in the shadows of ancient cities. Officia qui qui dolore aperiam saepe ab id nisi odio quod provident dicta dolores magnam.", 27.92m, 7, "Secret in Edinburgh" },
                    { 20, "A thrilling adventure featuring family and tradition, set in a world of magic and wonder. Porro ea eos et corrupti accusantium qui repudiandae.", 21.55m, 7, "Eternal Myth" },
                    { 21, "An unforgettable story following love and betrayal, in a time of great change. Voluptas et deleniti alias rerum rem quasi omnis qui necessitatibus.", 28.72m, 8, "The Bright Alexander" },
                    { 22, "A beautiful narrative exploring family and tradition, across mysterious landscapes. Aut quia sed officia totam tempore dolores quis optio consequatur dolorum cum sint.", 12.40m, 6, "Beyond the Journey" },
                    { 23, "A haunting story about friendship and loyalty, set in a world of magic and wonder. Necessitatibus id facere sapiente aut consequatur adipisci amet id expedita eaque voluptas rerum.", 17.61m, 5, "The Secret of London" },
                    { 24, "A thrilling adventure featuring love and betrayal, unfolding in the shadows of ancient cities. Corrupti rerum dolore non vel sed laudantium rem.", 12.12m, 9, "Tales of Shadowed Soul" },
                    { 25, "A thrilling adventure featuring courage and sacrifice, in a realm where nothing is as it seems. Eos consequuntur adipisci quis doloribus fugiat incidunt atque nulla et et laboriosam autem omnis tempore.", 18.94m, 10, "Beyond the Quest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A young wizard's journey begins at Hogwarts School of Witchcraft and Wizardry.", 19.99m, 1, "Harry Potter and the Philosopher's Stone" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A dystopian social science fiction novel about totalitarian surveillance.", 15.99m, 2, "1984" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A novel about racial injustice and childhood in the American South.", 14.99m, 2, "To Kill a Mockingbird" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A critique of the American Dream set in the Jazz Age.", 12.99m, 2, "The Great Gatsby" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A romantic novel about manners, upbringing, morality, and marriage.", 13.99m, 3, "Pride and Prejudice" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A boy's adventures along the Mississippi River.", 11.99m, 2, "The Adventures of Tom Sawyer" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "An aging Cuban fisherman's struggle with a giant marlin.", 10.99m, 2, "The Old Man and the Sea" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A detective story featuring Hercule Poirot on a luxury train.", 16.99m, 3, "Murder on the Orient Express" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A horror novel about a family isolated in a haunted hotel.", 18.99m, 4, "The Shining" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "Price", "PublisherId", "Title" },
                values: new object[] { "A story about the legacy of slavery and trauma.", 17.99m, 5, "Beloved" });
        }
    }
}
