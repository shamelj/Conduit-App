using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "science" },
                    { 2L, "programming" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Bio", "Email", "Image", "Password", "Username" },
                values: new object[,]
                {
                    { 1L, "", "shamel.com", "", "12345678", "shamel" },
                    { 2L, "", "Mohammed.com", "", "12345678", "mohammed" },
                    { 3L, "", "Ahmad.com", "", "12345678", "ahmad" }
                });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "Description", "Slug", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, "nice", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "interesting thumbnail", "c#", "c#", null },
                    { 2L, 2L, "nice", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "interesting thumbnail", "java", "Java", null },
                    { 3L, 3L, "nice", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "interesting thumbnail", "python", "Python", null }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "ArticleId", "AuthorId", "Body", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, "Nice article", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2L, 2L, 2L, "bad article", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3L, 3L, 3L, "meh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
