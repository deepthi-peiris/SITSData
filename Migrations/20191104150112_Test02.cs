using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SITSData.Migrations
{
    public partial class Test02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EngStudent",
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
                    FHEQLevel = table.Column<int>(nullable: false),
                    YearOfStudy = table.Column<int>(nullable: false),
                    UoPId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngStudent", x => x.Id);
                    table.UniqueConstraint("AK_EngStudent_StudentId", x => x.StudentId);
                    table.UniqueConstraint("AK_EngStudent_UoPId", x => x.UoPId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngStudent",
                schema: "test");
        }
    }
}
