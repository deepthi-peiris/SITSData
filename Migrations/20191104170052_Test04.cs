using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SITSData.Migrations
{
    public partial class Test04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngStudent",
                schema: "test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GSDStudent",
                schema: "test",
                table: "GSDStudent");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GSDStudent_StudentId",
                schema: "test",
                table: "GSDStudent");

            migrationBuilder.RenameTable(
                name: "GSDStudent",
                schema: "test",
                newName: "Student",
                newSchema: "test");

            migrationBuilder.AlterColumn<int>(
                name: "YearOfStudy",
                schema: "test",
                table: "Student",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EnglishLevel",
                schema: "test",
                table: "Student",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "test",
                table: "Student",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                schema: "test",
                table: "Student",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Student_StudentId",
                schema: "test",
                table: "Student",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                schema: "test",
                table: "Student");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Student_StudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "test",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                schema: "test",
                newName: "GSDStudent",
                newSchema: "test");

            migrationBuilder.AlterColumn<int>(
                name: "YearOfStudy",
                schema: "test",
                table: "GSDStudent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnglishLevel",
                schema: "test",
                table: "GSDStudent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GSDStudent",
                schema: "test",
                table: "GSDStudent",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GSDStudent_StudentId",
                schema: "test",
                table: "GSDStudent",
                column: "StudentId");

            migrationBuilder.CreateTable(
                name: "EngStudent",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CivilId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FHEQLevel = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PersonalTutor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UoPId = table.Column<int>(type: "int", nullable: false),
                    YearOfStudy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngStudent", x => x.Id);
                    table.UniqueConstraint("AK_EngStudent_StudentId", x => x.StudentId);
                    table.UniqueConstraint("AK_EngStudent_UoPId", x => x.UoPId);
                });
        }
    }
}
