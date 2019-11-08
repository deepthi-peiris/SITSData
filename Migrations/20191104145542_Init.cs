using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SITSData.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "test");

            migrationBuilder.CreateTable(
                name: "GSDStudent",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    Surname = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    CivilId = table.Column<int>(nullable: false),
                    Phone = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    StudentId = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(maxLength: 100, nullable: true),
                    PersonalTutor = table.Column<string>(maxLength: 150, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    EnglishLevel = table.Column<int>(nullable: false),
                    YearOfStudy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSDStudent", x => x.Id);
                    table.UniqueConstraint("AK_GSDStudent_StudentId", x => x.StudentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GSDStudent",
                schema: "test");
        }
    }
}
