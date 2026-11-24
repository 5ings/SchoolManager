using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolData.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Address", "CreatedOn", "Email", "IsDeleted", "Name", "PhoneNumber", "RoomsCount" },
                values: new object[,]
                {
                    { 1, "Казанлък", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pmg@abv.bg", false, "ПМГ Казанлък", "0431 123 456", 40 },
                    { 2, "Стара Загора", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ou@abv.bg", false, "ОУ Стара Загора", "042 123 457", 30 },
                    { 3, "Пловдив", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "su@abv.bg", false, "СУ Пловдив", "032 555 123", 50 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "ivanpetrov1@abv.bg", "Иван", "Петров", "0888 123 456" },
                    { 2, "petarivanov1@abv.bg", "Петър", "Иванов", "0888 123 457" },
                    { 3, "iordaniordanov1@abv.bg", "Йордан", "Йорданов", "0888 123 458" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "SchoolId", "Subject" },
                values: new object[,]
                {
                    { 1, "georgiivanov@abv.bg", "Георги", "Иванов", "0888 123 456", 1, "Математика" },
                    { 2, "mariamanolova@abv.bg", "Мария", "Манолова", "0888 123 457", 2, "Български език" },
                    { 3, "petardimitrov@abv.bg", "Петър", "Димитров", "0888 123 459", 3, "Физика" }
                });

            migrationBuilder.InsertData(
                table: "TeacherStudents",
                columns: new[] { "StudentId", "TeacherId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeacherStudents",
                keyColumns: new[] { "StudentId", "TeacherId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TeacherStudents",
                keyColumns: new[] { "StudentId", "TeacherId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "TeacherStudents",
                keyColumns: new[] { "StudentId", "TeacherId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
