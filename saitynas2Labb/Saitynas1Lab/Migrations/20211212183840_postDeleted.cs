using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynas1Lab.Migrations
{
    public partial class postDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Posts_PostId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PostId",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PostId",
                table: "Reviews",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Posts_PostId",
                table: "Reviews",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
