using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditAutditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_LastModifiedById",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LastModifiedById",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Books",
                newName: "LastModifiedByUserIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedByUserIds",
                table: "Books",
                newName: "LastModifiedById");

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
    }
}
