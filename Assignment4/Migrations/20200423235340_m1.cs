using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment4.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InterestedMajor",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestedMajor",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Student");
        }
    }
}
