using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRealtionsBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Academies_AcademyId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParents_Parents_ParentsId",
                table: "StudentParents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParents_Students_StudentsId",
                table: "StudentParents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentParents",
                table: "StudentParents");

            migrationBuilder.RenameTable(
                name: "StudentParents",
                newName: "ParentStudents");

            migrationBuilder.RenameIndex(
                name: "IX_StudentParents_StudentsId",
                table: "ParentStudents",
                newName: "IX_ParentStudents_StudentsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Teachers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "TokenOfAcademy",
                table: "Teachers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Students",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "TokenOfAcademy",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Classes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Classes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Admins",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "TokenOfAcademy",
                table: "Admins",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentStudents",
                table: "ParentStudents",
                columns: new[] { "ParentsId", "StudentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Academies_AcademyId",
                table: "Admins",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudents_Parents_ParentsId",
                table: "ParentStudents",
                column: "ParentsId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudents_Students_StudentsId",
                table: "ParentStudents",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Academies_AcademyId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudents_Parents_ParentsId",
                table: "ParentStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudents_Students_StudentsId",
                table: "ParentStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentStudents",
                table: "ParentStudents");

            migrationBuilder.DropColumn(
                name: "TokenOfAcademy",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TokenOfAcademy",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TokenOfAcademy",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "ParentStudents",
                newName: "StudentParents");

            migrationBuilder.RenameIndex(
                name: "IX_ParentStudents_StudentsId",
                table: "StudentParents",
                newName: "IX_StudentParents_StudentsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Teachers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Classes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Classes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademyId",
                table: "Admins",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentParents",
                table: "StudentParents",
                columns: new[] { "ParentsId", "StudentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Academies_AcademyId",
                table: "Admins",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParents_Parents_ParentsId",
                table: "StudentParents",
                column: "ParentsId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParents_Students_StudentsId",
                table: "StudentParents",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
