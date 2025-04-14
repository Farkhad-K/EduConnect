using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTokenForTeacherEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenOfAcademy",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UniqueToken",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TokenOfAcademy",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "TokenForTeachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "varchar(6)", nullable: true),
                    AcademyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenForTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenForTeachers_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokenForTeachers_AcademyId",
                table: "TokenForTeachers",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenForTeachers_Token",
                table: "TokenForTeachers",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenForTeachers");

            migrationBuilder.AddColumn<string>(
                name: "TokenOfAcademy",
                table: "Teachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueToken",
                table: "Teachers",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TokenOfAcademy",
                table: "Students",
                type: "text",
                nullable: true);
        }
    }
}
