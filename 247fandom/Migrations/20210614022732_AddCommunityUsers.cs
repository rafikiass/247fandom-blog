using Microsoft.EntityFrameworkCore.Migrations;

namespace _247fandom.Migrations
{
    public partial class AddCommunityUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunityUser",
                columns: table => new
                {
                    MembersUserId = table.Column<long>(type: "bigint", nullable: false),
                    MembershipsCommunityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityUser", x => new { x.MembersUserId, x.MembershipsCommunityId });
                    table.ForeignKey(
                        name: "FK_CommunityUser_Community_MembershipsCommunityId",
                        column: x => x.MembershipsCommunityId,
                        principalTable: "Community",
                        principalColumn: "CommunityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityUser_Users_MembersUserId",
                        column: x => x.MembersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityUser_MembershipsCommunityId",
                table: "CommunityUser",
                column: "MembershipsCommunityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityUser");
        }
    }
}
