using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JKLHealthcare.Migrations
{
  
    public partial class MakeCaregiverIdNullable : Migration
    {
       
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Caregivers_CaregiverId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.AlterColumn<int>(
                name: "CaregiverId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Caregivers_CaregiverId",
                table: "Appointments",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id");
        }

       
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Caregivers_CaregiverId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "PatientID");

            migrationBuilder.AlterColumn<int>(
                name: "CaregiverId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Caregivers_CaregiverId",
                table: "Appointments",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
