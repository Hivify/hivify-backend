using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Complaints.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedComplaintsToRefernceAssociationID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Complaints");

            migrationBuilder.AddColumn<Guid>(
                name: "AssociationId",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "Complaints");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
