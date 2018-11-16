using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class changeKeyForStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentID",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentID",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "StudentSubjects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentNumber",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentNumber",
                table: "StudentSubjects",
                column: "StudentNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentNumber",
                table: "StudentSubjects",
                column: "StudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudentNumber",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentNumber",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "StudentSubjects");

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "StudentSubjects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentNumber",
                table: "Students",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Students",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentID",
                table: "StudentSubjects",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudentID",
                table: "StudentSubjects",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
