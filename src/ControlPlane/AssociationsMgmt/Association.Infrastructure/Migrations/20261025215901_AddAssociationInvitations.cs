using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Association.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssociationInvitations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssociationInvitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitedByUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationInvitations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociationInvitations_AssociationID",
                table: "AssociationInvitations",
                column: "AssociationID");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationInvitations_TokenHash",
                table: "AssociationInvitations",
                column: "TokenHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationInvitations");
        }
    }
}
