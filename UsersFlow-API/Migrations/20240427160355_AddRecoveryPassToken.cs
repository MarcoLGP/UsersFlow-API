using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersFlow_API.Migrations
{
    /// <inheritdoc />
    public partial class AddRecoveryPassToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRecoveryPassTokens",
                columns: table => new
                {
                    RecoveryPassTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RecoveryPassToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecoveryPassTokens", x => x.RecoveryPassTokenId);
                    table.ForeignKey(
                        name: "FK_UserRecoveryPassTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRecoveryPassTokens_UserId",
                table: "UserRecoveryPassTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRecoveryPassTokens");
        }
    }
}
