using Microsoft.EntityFrameworkCore.Migrations;

namespace NLayerApp.DataAccessLayer.Migrations
{
    public partial class modifyingmanyToManyrelationofGroupsandMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GroupMembers_MemberId",
                table: "GroupMembers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_GroupMembers_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GroupMembers_MemberId",
                table: "GroupMembers",
                column: "MemberId");
        }
    }
}
