using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogIn",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogIn", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tuitionOutState = table.Column<int>(nullable: true),
                    schoolZip = table.Column<string>(nullable: true),
                    schoolUrl = table.Column<string>(nullable: true),
                    accCode = table.Column<string>(nullable: true),
                    schoolName = table.Column<string>(nullable: true),
                    studentSize = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    contact = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    StudentEmail = table.Column<string>(nullable: false),
                    University = table.Column<string>(nullable: true),
                    Major = table.Column<string>(nullable: true),
                    StudentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.StudentEmail);
                    table.ForeignKey(
                        name: "FK_Applications_Student_StudentName",
                        column: x => x.StudentName,
                        principalTable: "Student",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StudentName",
                table: "Applications",
                column: "StudentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "LogIn");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
