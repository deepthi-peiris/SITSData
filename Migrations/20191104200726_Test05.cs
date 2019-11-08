using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SITSData.Migrations
{
    public partial class Test05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishLevel",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "YearOfStudy",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PersonalTutor",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "test",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "EngEntryRecordStudentId",
                schema: "test",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntryType",
                schema: "test",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UoPId",
                schema: "test",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GSDEntryRecordStudentId",
                schema: "test",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnrolled",
                schema: "test",
                table: "Student",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EngEntryRecord",
                schema: "test",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    BasicMathematics = table.Column<int>(nullable: true),
                    PureMathematics = table.Column<int>(nullable: true),
                    Physics = table.Column<int>(nullable: true),
                    Computing = table.Column<int>(nullable: true),
                    BS = table.Column<double>(nullable: true),
                    W = table.Column<double>(nullable: true),
                    S = table.Column<double>(nullable: true),
                    L = table.Column<double>(nullable: true),
                    R = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngEntryRecord", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "GSDEntryRecord",
                schema: "test",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    Maths = table.Column<int>(nullable: false),
                    Physics = table.Column<int>(nullable: false),
                    English = table.Column<int>(nullable: false),
                    EntryLevel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSDEntryRecord", x => x.StudentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_EngEntryRecordStudentId",
                schema: "test",
                table: "Student",
                column: "EngEntryRecordStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GSDEntryRecordStudentId",
                schema: "test",
                table: "Student",
                column: "GSDEntryRecordStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_EngEntryRecord_EngEntryRecordStudentId",
                schema: "test",
                table: "Student",
                column: "EngEntryRecordStudentId",
                principalSchema: "test",
                principalTable: "EngEntryRecord",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_GSDEntryRecord_GSDEntryRecordStudentId",
                schema: "test",
                table: "Student",
                column: "GSDEntryRecordStudentId",
                principalSchema: "test",
                principalTable: "GSDEntryRecord",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_EngEntryRecord_EngEntryRecordStudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_GSDEntryRecord_GSDEntryRecordStudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropTable(
                name: "EngEntryRecord",
                schema: "test");

            migrationBuilder.DropTable(
                name: "GSDEntryRecord",
                schema: "test");

            migrationBuilder.DropIndex(
                name: "IX_Student_EngEntryRecordStudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_GSDEntryRecordStudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EngEntryRecordStudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EntryType",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "UoPId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GSDEntryRecordStudentId",
                schema: "test",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DateEnrolled",
                schema: "test",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "EnglishLevel",
                schema: "test",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfStudy",
                schema: "test",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "test",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalTutor",
                schema: "test",
                table: "Student",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "test",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
