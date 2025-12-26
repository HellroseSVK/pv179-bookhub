using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddBookAuditLogging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EditCount",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "EditCount", "LastModifiedById" },
                values: new object[] { 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastModifiedById",
                table: "Books",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_LastModifiedById",
                table: "Books",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_LastModifiedById",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LastModifiedById",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EditCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Books");
        }
    }
}
