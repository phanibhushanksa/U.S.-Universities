using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    uId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tuitionOutState = table.Column<int>(nullable: true),
                    schoolCity = table.Column<string>(nullable: true),
                    schoolUrl = table.Column<string>(nullable: true),
                    accCode = table.Column<string>(nullable: true),
                    schoolName = table.Column<string>(nullable: true),
                    studentSize = table.Column<int>(nullable: true),
                    likesCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.uId);
                });

            migrationBuilder.CreateTable(
                name: "SignUp",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    cNumber = table.Column<string>(nullable: true),
                    university = table.Column<string>(nullable: true),
                    major = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignUp", x => x.email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "SignUp");
        }
    }
}
