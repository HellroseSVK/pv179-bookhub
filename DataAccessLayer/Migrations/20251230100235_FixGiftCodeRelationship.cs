using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixGiftCodeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftCodes_Orders_OrderId2",
                table: "GiftCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_GiftCodes_GiftCodeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GiftCodeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_GiftCodes_OrderId2",
                table: "GiftCodes");

            migrationBuilder.DropColumn(
                name: "GiftCodeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId2",
                table: "GiftCodes");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "UserId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "UserId",
                value: 7);

            migrationBuilder.CreateIndex(
                name: "IX_GiftCodes_OrderId",
                table: "GiftCodes",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCodes_Orders_OrderId",
                table: "GiftCodes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftCodes_Orders_OrderId",
                table: "GiftCodes");

            migrationBuilder.DropIndex(
                name: "IX_GiftCodes_OrderId",
                table: "GiftCodes");

            migrationBuilder.AddColumn<int>(
                name: "GiftCodeId",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId2",
                table: "GiftCodes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 5,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 6,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 7,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 8,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 9,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 10,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 11,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 12,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 13,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 14,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 15,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 17,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 18,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 19,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 20,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 21,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 22,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 23,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 24,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 25,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 26,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 27,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 28,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 29,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 30,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 31,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 32,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 33,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 34,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 35,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 36,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 37,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 38,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 39,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 40,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 41,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 42,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 43,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 44,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 45,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 46,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 47,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 48,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 49,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 50,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 51,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 52,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 53,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 54,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 55,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 56,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 57,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 58,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 59,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 60,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 61,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 62,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 63,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 64,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 65,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 66,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 67,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 68,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 69,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 70,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 71,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 72,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 73,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 74,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 75,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 76,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 77,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 78,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 79,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 80,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 81,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 82,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 83,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 84,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 85,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 86,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 87,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 88,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 89,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 90,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 91,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 92,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 93,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 94,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 95,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 96,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 97,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 98,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 99,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 100,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 101,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 102,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 103,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 104,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 105,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 106,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 107,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 108,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 109,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 110,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 111,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 112,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 113,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 114,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 115,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 116,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 117,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 118,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 119,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 120,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 121,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 122,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 123,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 124,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 125,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 126,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 127,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 128,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 129,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 130,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 131,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 132,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 133,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 134,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 135,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 136,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 137,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 138,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 139,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 140,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 141,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 142,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 143,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 144,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 145,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 146,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 147,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 148,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 149,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 150,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 151,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 152,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 153,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 154,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 155,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 156,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 157,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 158,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 159,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 160,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 161,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 162,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 163,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 164,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 165,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 166,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 167,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 168,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 169,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 170,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 171,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 172,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 173,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 174,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 175,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 176,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 177,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 178,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 179,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 180,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 181,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 182,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 183,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 184,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 185,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 186,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 187,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 188,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 189,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 190,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 191,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 192,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 193,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 194,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 195,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 196,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 197,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 198,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 199,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 200,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 201,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 202,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 203,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 204,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 205,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 206,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 207,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 208,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 209,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 210,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 211,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 212,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 213,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 214,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 215,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 216,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 217,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 218,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 219,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 220,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 221,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 222,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 223,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 224,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 225,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 226,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 227,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 228,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 229,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 230,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 231,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 232,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 233,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 234,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 235,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 236,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 237,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 238,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 239,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 240,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 241,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 242,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 243,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 244,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 245,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 246,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 247,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 248,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 249,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 250,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 251,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 252,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 253,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 254,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 255,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 256,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 257,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 258,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 259,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 260,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 261,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 262,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 263,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 264,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 265,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 266,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 267,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 268,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 269,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 270,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 271,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 272,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 273,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 274,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 275,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 276,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 277,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 278,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 279,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 280,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 281,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 282,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 283,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 284,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 285,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 286,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 287,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 288,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 289,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 290,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 291,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 292,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 293,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 294,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 295,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 296,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 297,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 298,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 299,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 300,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 301,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 302,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 303,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 304,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 305,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 306,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 307,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 308,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 309,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 310,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 311,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 312,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 313,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 314,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 315,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 316,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 317,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 318,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 319,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 320,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 321,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 322,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 323,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 324,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 325,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 326,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 327,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 328,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 329,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 330,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 331,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 332,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 333,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 334,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 335,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 336,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 337,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 338,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 339,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 340,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 341,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 342,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 343,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 344,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 345,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 346,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 347,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 348,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 349,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 350,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 351,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 352,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 353,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 354,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 355,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 356,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 357,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 358,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 359,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 360,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 361,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 362,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 363,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 364,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 365,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 366,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 367,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 368,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 369,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 370,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 371,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 372,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 373,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 374,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 375,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 376,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 377,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 378,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 379,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 380,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 381,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 382,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 383,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 384,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 385,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 386,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 387,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 388,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 389,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 390,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 391,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 392,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 393,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 394,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 395,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 396,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 397,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 398,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 399,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 400,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 401,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 402,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 403,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 404,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 405,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 406,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 407,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 408,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 409,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 410,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 411,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 412,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 413,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 414,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 415,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 416,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 417,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 418,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 419,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 420,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 421,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 422,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 423,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 424,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 425,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 426,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 427,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 428,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 429,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 430,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 431,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 432,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 433,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 434,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 435,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 436,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 437,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 438,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 439,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 440,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 441,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 442,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 443,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 444,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 445,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 446,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 447,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 448,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 449,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 450,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 451,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 452,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 453,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 454,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 455,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 456,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 457,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 458,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 459,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 460,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 461,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 462,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 463,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 464,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 465,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 466,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 467,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 468,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 469,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 470,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 471,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 472,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 473,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 474,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 475,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 476,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 477,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 478,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 479,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 480,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 481,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 482,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 483,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 484,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 485,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 486,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 487,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 488,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 489,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 490,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 491,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 492,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 493,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 494,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 495,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 496,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 497,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 498,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 499,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "GiftCodes",
                keyColumn: "Id",
                keyValue: 500,
                column: "OrderId2",
                value: null);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "GiftCodeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 5 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 3 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 6 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 8 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 8 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 3 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "GiftCodeId", "UserId" },
                values: new object[] { null, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GiftCodeId",
                table: "Orders",
                column: "GiftCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftCodes_OrderId2",
                table: "GiftCodes",
                column: "OrderId2");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCodes_Orders_OrderId2",
                table: "GiftCodes",
                column: "OrderId2",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_GiftCodes_GiftCodeId",
                table: "Orders",
                column: "GiftCodeId",
                principalTable: "GiftCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
