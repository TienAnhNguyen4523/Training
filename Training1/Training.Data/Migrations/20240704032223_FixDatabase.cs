using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExportBill_BookId",
                table: "ExportBill");

            migrationBuilder.DropIndex(
                name: "IX_ExportBill_UserId",
                table: "ExportBill");

            migrationBuilder.DropIndex(
                name: "IX_BorrowAndReturnBook_BookId",
                table: "BorrowAndReturnBook");

            migrationBuilder.DropIndex(
                name: "IX_BorrowAndReturnBook_UserId",
                table: "BorrowAndReturnBook");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CategoryId",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_ExportBill_BookId",
                table: "ExportBill",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportBill_UserId",
                table: "ExportBill",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowAndReturnBook_BookId",
                table: "BorrowAndReturnBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowAndReturnBook_UserId",
                table: "BorrowAndReturnBook",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExportBill_BookId",
                table: "ExportBill");

            migrationBuilder.DropIndex(
                name: "IX_ExportBill_UserId",
                table: "ExportBill");

            migrationBuilder.DropIndex(
                name: "IX_BorrowAndReturnBook_BookId",
                table: "BorrowAndReturnBook");

            migrationBuilder.DropIndex(
                name: "IX_BorrowAndReturnBook_UserId",
                table: "BorrowAndReturnBook");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_CategoryId",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_ExportBill_BookId",
                table: "ExportBill",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExportBill_UserId",
                table: "ExportBill",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowAndReturnBook_BookId",
                table: "BorrowAndReturnBook",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowAndReturnBook_UserId",
                table: "BorrowAndReturnBook",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId",
                unique: true);
        }
    }
}
