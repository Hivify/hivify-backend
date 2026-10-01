using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Department.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedToLoadMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Departments_DepartmentEntityId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_DepartmentEntityId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DepartmentEntityId",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentEntityId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_DepartmentEntityId",
                table: "Members",
                column: "DepartmentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Departments_DepartmentEntityId",
                table: "Members",
                column: "DepartmentEntityId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
