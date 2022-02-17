using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker.Migrations
{
    public partial class AddBackIssueLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: "Issue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineMet",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Issue");
        }
    }
}
