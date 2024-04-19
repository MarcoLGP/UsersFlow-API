using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersFlow_API.Migrations
{
    /// <inheritdoc />
    public partial class Add_Locked_and_Public_Note : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locked",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "Notes");
        }
    }
}
