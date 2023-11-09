using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Store.Migrations.OpenAIChat
{
    /// <inheritdoc />
    public partial class RedesigntheTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ChatMessages",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "ChatMessages",
                newName: "Message");
        }
    }
}
