using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class temp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowUser_User_FollowedId",
                table: "UserFollowUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowUser_User_FollowerId",
                table: "UserFollowUser");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowUser_FollowerId_FollowedId",
                table: "UserFollowUser");

            migrationBuilder.AlterColumn<long>(
                name: "FollowerId",
                table: "UserFollowUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FollowedId",
                table: "UserFollowUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowUser_FollowerId_FollowedId",
                table: "UserFollowUser",
                columns: new[] { "FollowerId", "FollowedId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowUser_User_FollowedId",
                table: "UserFollowUser",
                column: "FollowedId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowUser_User_FollowerId",
                table: "UserFollowUser",
                column: "FollowerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowUser_User_FollowedId",
                table: "UserFollowUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowUser_User_FollowerId",
                table: "UserFollowUser");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowUser_FollowerId_FollowedId",
                table: "UserFollowUser");

            migrationBuilder.AlterColumn<long>(
                name: "FollowerId",
                table: "UserFollowUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FollowedId",
                table: "UserFollowUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowUser_FollowerId_FollowedId",
                table: "UserFollowUser",
                columns: new[] { "FollowerId", "FollowedId" },
                unique: true,
                filter: "[FollowerId] IS NOT NULL AND [FollowedId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowUser_User_FollowedId",
                table: "UserFollowUser",
                column: "FollowedId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowUser_User_FollowerId",
                table: "UserFollowUser",
                column: "FollowerId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
