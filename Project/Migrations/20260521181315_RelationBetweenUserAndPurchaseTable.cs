using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class RelationBetweenUserAndPurchaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRecords_UserId",
                table: "PurchaseRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRecords_Users_UserId",
                table: "PurchaseRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRecords_Users_UserId",
                table: "PurchaseRecords");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRecords_UserId",
                table: "PurchaseRecords");
        }
    }
}
