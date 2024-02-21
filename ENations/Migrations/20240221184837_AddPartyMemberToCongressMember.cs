using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENations.Migrations
{
    /// <inheritdoc />
    public partial class AddPartyMemberToCongressMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CongressMembers_PoliticalParties_PoliticalPartyId",
                table: "CongressMembers");

            migrationBuilder.AlterColumn<int>(
                name: "PoliticalPartyId",
                table: "CongressMembers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PartyMemberId",
                table: "CongressMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CongressMembers_PartyMemberId",
                table: "CongressMembers",
                column: "PartyMemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CongressMembers_PartyMembers_PartyMemberId",
                table: "CongressMembers",
                column: "PartyMemberId",
                principalTable: "PartyMembers",
                principalColumn: "PartyMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CongressMembers_PoliticalParties_PoliticalPartyId",
                table: "CongressMembers",
                column: "PoliticalPartyId",
                principalTable: "PoliticalParties",
                principalColumn: "PoliticalPartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CongressMembers_PartyMembers_PartyMemberId",
                table: "CongressMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CongressMembers_PoliticalParties_PoliticalPartyId",
                table: "CongressMembers");

            migrationBuilder.DropIndex(
                name: "IX_CongressMembers_PartyMemberId",
                table: "CongressMembers");

            migrationBuilder.DropColumn(
                name: "PartyMemberId",
                table: "CongressMembers");

            migrationBuilder.AlterColumn<int>(
                name: "PoliticalPartyId",
                table: "CongressMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CongressMembers_PoliticalParties_PoliticalPartyId",
                table: "CongressMembers",
                column: "PoliticalPartyId",
                principalTable: "PoliticalParties",
                principalColumn: "PoliticalPartyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
