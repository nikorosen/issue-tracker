using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker.Migrations
{
    public partial class ModifyIssueNavProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_AspNetUsers_UserId1",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_UserId1",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Issue");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Issue",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issue_UserId",
                table: "Issue",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_AspNetUsers_UserId",
                table: "Issue",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_AspNetUsers_UserId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_UserId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Issue");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Issue",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Issue",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issue_UserId1",
                table: "Issue",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_AspNetUsers_UserId1",
                table: "Issue",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
