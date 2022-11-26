using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedseed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Body", "Slug", "Title" },
                values: new object[] { "elegant language", "c-sharp", "C-sharp" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Body", "Description" },
                values: new object[] { "very long body", "boring thumbnail" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Body", "Description", "Slug", "Title" },
                values: new object[] { "having a hard time", "", "earth", "Earth" });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ArticleId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ArticleId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Body",
                value: "good to know");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "legacy code");

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3L, "nature" },
                    { 4L, "education" },
                    { 5L, "food" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Bio", "Email", "Image", "Password", "Username" },
                values: new object[,]
                {
                    { 4L, "", "ali.com", "", "12345678", "ali" },
                    { 5L, "", "amal.com", "", "12345678", "amal" }
                });

            migrationBuilder.InsertData(
                table: "UserFavouriteArticle",
                columns: new[] { "Id", "ArticleId", "UserId" },
                values: new object[,]
                {
                    { 1L, 3L, 1L },
                    { 2L, 1L, 1L },
                    { 4L, 2L, 3L }
                });

            migrationBuilder.InsertData(
                table: "UserFollowUser",
                columns: new[] { "Id", "FollowedId", "FollowerId" },
                values: new object[,]
                {
                    { 1L, 2L, 1L },
                    { 2L, 3L, 2L }
                });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "Description", "Slug", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 4L, 4L, "Do your best", null, "What you need to know", "university-life", "University Life", null },
                    { 5L, 5L, "Very delicious", null, "try it once love it forever", "spaghetti", "Spaghetti", null }
                });

            migrationBuilder.InsertData(
                table: "UserFavouriteArticle",
                columns: new[] { "Id", "ArticleId", "UserId" },
                values: new object[] { 5L, 3L, 4L });

            migrationBuilder.InsertData(
                table: "UserFollowUser",
                columns: new[] { "Id", "FollowedId", "FollowerId" },
                values: new object[,]
                {
                    { 3L, 4L, 3L },
                    { 4L, 5L, 4L },
                    { 5L, 1L, 5L }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "ArticleId", "AuthorId", "Body", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 4L, 4L, 5L, "interesting", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5L, 5L, 3L, "looks delicious", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "UserFavouriteArticle",
                columns: new[] { "Id", "ArticleId", "UserId" },
                values: new object[] { 3L, 4L, 2L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "UserFavouriteArticle",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserFavouriteArticle",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UserFavouriteArticle",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UserFavouriteArticle",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "UserFavouriteArticle",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "UserFollowUser",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserFollowUser",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UserFollowUser",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UserFollowUser",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "UserFollowUser",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Body", "Slug", "Title" },
                values: new object[] { "nice", "c#", "c#" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Body", "Description" },
                values: new object[] { "nice", "interesting thumbnail" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Body", "Description", "Slug", "Title" },
                values: new object[] { "nice", "interesting thumbnail", "python", "Python" });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ArticleId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ArticleId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Body",
                value: "meh");

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "science");
        }
    }
}
