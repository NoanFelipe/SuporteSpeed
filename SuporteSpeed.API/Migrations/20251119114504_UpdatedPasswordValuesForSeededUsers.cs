using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuporteSpeed.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPasswordValuesForSeededUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cfaa508f-4817-4149-9d96-18de505c1be8",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEENWXDp+lc3xmS1O+j+KsXlpVHbTeLZQo0/dCYx+tXwlkr9XnUFVe7Ljw6h7au0SyA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d5c07402-2935-48e1-a9c1-fe50ea56c080",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEENWXDp+lc3xmS1O+j+KsXlpVHbTeLZQo0/dCYx+tXwlkr9XnUFVe7Ljw6h7au0SyA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cfaa508f-4817-4149-9d96-18de505c1be8",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDrlZiaM0hQNgv7cECrM5Eyxf0XUgdG9pgGZvJObHflPXlj0xdNjUgm8hmT7jCxzQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d5c07402-2935-48e1-a9c1-fe50ea56c080",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDrlZiaM0hQNgv7cECrM5Eyxf0XUgdG9pgGZvJObHflPXlj0xdNjUgm8hmT7jCxzQ==");
        }
    }
}
