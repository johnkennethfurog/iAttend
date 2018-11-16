using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IAttend.API.Migrations
{
    public partial class changeInstrucorPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Instructors_InstructorID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_InstructorID",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "InstructorID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Instructors");

            migrationBuilder.AddColumn<string>(
                name: "InstructorNumber",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorNumber",
                table: "Instructors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "InstructorNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_InstructorNumber",
                table: "Schedules",
                column: "InstructorNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Instructors_InstructorNumber",
                table: "Schedules",
                column: "InstructorNumber",
                principalTable: "Instructors",
                principalColumn: "InstructorNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Instructors_InstructorNumber",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_InstructorNumber",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "InstructorNumber",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "InstructorID",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorNumber",
                table: "Instructors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Instructors",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_InstructorID",
                table: "Schedules",
                column: "InstructorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Instructors_InstructorID",
                table: "Schedules",
                column: "InstructorID",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
