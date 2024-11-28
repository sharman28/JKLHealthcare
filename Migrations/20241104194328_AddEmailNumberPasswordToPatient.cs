﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JKLHealthcare.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailNumberPasswordToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patients",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Patients",
                newName: "Name");
        }
    }
}
