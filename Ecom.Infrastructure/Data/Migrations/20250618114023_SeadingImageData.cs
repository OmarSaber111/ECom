using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeadingImageData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ImgName", "ProductId" },
                values: new object[] { 2, "test", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
