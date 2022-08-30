using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class RENAMING_PassworResetToken_AND_PassworResetTokenExpiresAt_TO_PasswordResetToken_AND_PasswordResetTokenExpiresAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassworResetTokenExpiresAt",
                table: "Users",
                newName: "PasswordResetTokenExpiresAt");

            migrationBuilder.RenameColumn(
                name: "PassworResetToken",
                table: "Users",
                newName: "PasswordResetToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiresAt",
                table: "Users",
                newName: "PassworResetTokenExpiresAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Users",
                newName: "PassworResetToken");
        }
    }
}
