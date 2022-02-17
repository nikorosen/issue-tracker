using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker.Migrations
{
    public partial class RemoveIssueLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineMet",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Issue");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Issue",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Issue");

            migrationBuilder.AddColumn<bool>(
                name: "DeadlineMet",
                table: "Issue",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
