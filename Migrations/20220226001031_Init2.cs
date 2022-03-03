using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Project_1_ProjectId",
                table: "Issue");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Issue",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Project_1_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Project_1",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Project_1_ProjectId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Issue");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Issue",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Project_1_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Project_1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
