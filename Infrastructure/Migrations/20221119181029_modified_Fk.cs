using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifiedFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Article",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowUser_FollowerId_FollowedId",
                table: "UserFollowUser",
                columns: new[] { "FollowerId", "FollowedId" },
                unique: true);

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Article",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowUser_FollowerId_FollowedId",
                table: "UserFollowUser",
                columns: new[] { "FollowerId", "FollowedId" },
                unique: true,
                filter: "[FollowerId] IS NOT NULL AND [FollowedId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowUser_User_FollowerId",
                table: "UserFollowUser",
                column: "FollowerId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
