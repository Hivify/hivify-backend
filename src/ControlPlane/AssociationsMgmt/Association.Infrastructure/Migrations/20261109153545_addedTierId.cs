using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Association.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedTierId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TierId",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TierId",
                table: "Associations");
        }
    }
}
