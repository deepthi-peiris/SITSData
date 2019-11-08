using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SITSData.Migrations
{
    public partial class Test06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IELTSResultsId",
                schema: "test",
                table: "EngEntryRecord",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IELTSResults",
                schema: "test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    FirstNames = table.Column<string>(nullable: true),
                    FamilyName = table.Column<string>(nullable: true),
                    DateTaken = table.Column<DateTime>(nullable: false),
                    L = table.Column<double>(nullable: false),
                    R = table.Column<double>(nullable: false),
                    W = table.Column<double>(nullable: false),
                    S = table.Column<double>(nullable: false),
                    BS = table.Column<double>(nullable: false),
                    DateSubmitted = table.Column<DateTime>(nullable: true),
                    DateVerified = table.Column<DateTime>(nullable: true),
                    VerifiedBy = table.Column<string>(nullable: true),
                    ScannedCopy = table.Column<string>(maxLength: 1, nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(maxLength: 1, nullable: true),
                    Class = table.Column<string>(maxLength: 5, nullable: true),
                    PhoneNumber = table.Column<int>(nullable: true),
                    CivilID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IELTSResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngEntryRecord_IELTSResultsId",
                schema: "test",
                table: "EngEntryRecord",
                column: "IELTSResultsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngEntryRecord_IELTSResults_IELTSResultsId",
                schema: "test",
                table: "EngEntryRecord",
                column: "IELTSResultsId",
                principalSchema: "test",
                principalTable: "IELTSResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngEntryRecord_IELTSResults_IELTSResultsId",
                schema: "test",
                table: "EngEntryRecord");

            migrationBuilder.DropTable(
                name: "IELTSResults",
                schema: "test");

            migrationBuilder.DropIndex(
                name: "IX_EngEntryRecord_IELTSResultsId",
                schema: "test",
                table: "EngEntryRecord");

            migrationBuilder.DropColumn(
                name: "IELTSResultsId",
                schema: "test",
                table: "EngEntryRecord");
        }
    }
}
