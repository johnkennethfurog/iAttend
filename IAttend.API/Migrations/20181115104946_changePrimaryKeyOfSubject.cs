using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class changePrimaryKeyOfSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subjects_SubjectID",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubjectID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "Schedules");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectCode",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectCode",
                table: "Schedules",
                column: "SubjectCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subjects_SubjectCode",
                table: "Schedules",
                column: "SubjectCode",
                principalTable: "Subjects",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Subjects_SubjectCode",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubjectCode",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SubjectCode",
                table: "Schedules");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Subjects",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectID",
                table: "Schedules",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Subjects_SubjectID",
                table: "Schedules",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
