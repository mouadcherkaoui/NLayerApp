using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NLayerApp.DataAccessLayer.Migrations.AppDb
{
    public partial class newdatamodelmanyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Member_MemberId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Group_GroupId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_GroupId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Group_MemberId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Group");

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => new { x.GroupId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_GroupMembers_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_MemberId",
                table: "GroupMembers",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Member",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Group",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_GroupId",
                table: "Member",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_MemberId",
                table: "Group",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Member_MemberId",
                table: "Group",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Group_GroupId",
                table: "Member",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
