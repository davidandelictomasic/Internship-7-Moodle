using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoodleApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "date_of_birth", "email", "name", "password", "role" },
                values: new object[,]
                {
                    { 1, new DateOnly(1980, 1, 15), "admin@moodle.com", "Admin User", "admin123", 2 },
                    { 2, new DateOnly(1975, 5, 20), "ivan.horvat@moodle.com", "Dr. Ivan Horvat", "prof123", 1 },
                    { 3, new DateOnly(1982, 8, 10), "ana.kovac@moodle.com", "Dr. Ana Kovač", "prof123", 1 },
                    { 4, new DateOnly(1978, 3, 25), "marko.babic@moodle.com", "Dr. Marko Babić", "prof123", 1 },
                    { 5, new DateOnly(2000, 4, 12), "petra.novak@student.moodle.com", "Petra Novak", "student123", 0 },
                    { 6, new DateOnly(2001, 7, 8), "luka.juric@student.moodle.com", "Luka Jurić", "student123", 0 },
                    { 7, new DateOnly(1999, 11, 30), "maja.peric@student.moodle.com", "Maja Perić", "student123", 0 },
                    { 8, new DateOnly(2002, 2, 14), "tomislav.knezevic@student.moodle.com", "Tomislav Knežević", "student123", 0 },
                    { 9, new DateOnly(2000, 9, 5), "sara.matic@student.moodle.com", "Sara Matić", "student123", 0 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "chatrooms",
                columns: new[] { "id", "first_user_id", "second_user_id" },
                values: new object[,]
                {
                    { 1, 5, 2 },
                    { 2, 6, 3 },
                    { 3, 5, 6 },
                    { 4, 7, 4 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "courses",
                columns: new[] { "id", "name", "professor_id" },
                values: new object[,]
                {
                    { 1, "Programiranje u C#", 2 },
                    { 2, "Baze podataka", 2 },
                    { 3, "Web razvoj", 3 },
                    { 4, "Algoritmi i strukture podataka", 3 },
                    { 5, "Računalne mreže", 4 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "announcements",
                columns: new[] { "id", "content", "course_id", "created_at", "professor_id", "title" },
                values: new object[,]
                {
                    { 1, "Dobrodošli na kolegij Programiranje u C#! Prvi sat je u ponedjeljak.", 1, new DateTime(2025, 9, 1, 8, 0, 0, 0, DateTimeKind.Utc), 2, "Dobrodošli na kolegij" },
                    { 2, "Prva zadaća je objavljena. Rok za predaju je za tjedan dana.", 1, new DateTime(2025, 9, 8, 10, 0, 0, 0, DateTimeKind.Utc), 2, "Zadaća 1" },
                    { 3, "Molim vas instalirajte PostgreSQL prije sljedećeg predavanja.", 2, new DateTime(2025, 9, 2, 9, 0, 0, 0, DateTimeKind.Utc), 2, "Instalacija PostgreSQL" },
                    { 4, "Odaberite temu za završni projekt do kraja mjeseca.", 3, new DateTime(2025, 9, 15, 14, 0, 0, 0, DateTimeKind.Utc), 3, "Projekt - teme" },
                    { 5, "Prvi kolokvij je zakazan za 15. listopada.", 4, new DateTime(2025, 9, 20, 11, 0, 0, 0, DateTimeKind.Utc), 3, "Kolokvij 1" },
                    { 6, "Laboratorijske vježbe počinju sljedeći tjedan.", 5, new DateTime(2025, 9, 10, 13, 0, 0, 0, DateTimeKind.Utc), 4, "Laboratorijske vježbe" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "course_students",
                columns: new[] { "CourseId", "UserId", "EnrolledAt" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2025, 9, 1, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 5, new DateTime(2025, 9, 1, 10, 15, 0, 0, DateTimeKind.Utc) },
                    { 4, 5, new DateTime(2025, 9, 4, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 6, new DateTime(2025, 9, 1, 11, 30, 0, 0, DateTimeKind.Utc) },
                    { 3, 6, new DateTime(2025, 9, 2, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 6, new DateTime(2025, 9, 4, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 1, 7, new DateTime(2025, 9, 2, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 7, new DateTime(2025, 9, 2, 8, 30, 0, 0, DateTimeKind.Utc) },
                    { 5, 7, new DateTime(2025, 9, 5, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 8, new DateTime(2025, 9, 3, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 8, new DateTime(2025, 9, 2, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 8, new DateTime(2025, 9, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 9, new DateTime(2025, 9, 3, 15, 30, 0, 0, DateTimeKind.Utc) },
                    { 3, 9, new DateTime(2025, 9, 2, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 9, new DateTime(2025, 9, 5, 11, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "materials",
                columns: new[] { "id", "added_at", "course_id", "name", "url" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 1, 8, 0, 0, 0, DateTimeKind.Utc), 1, "Uvod u C#", "https://moodle.com/materials/csharp-intro.pdf" },
                    { 2, new DateTime(2025, 9, 5, 9, 0, 0, 0, DateTimeKind.Utc), 1, "OOP koncepti", "https://moodle.com/materials/oop-concepts.pdf" },
                    { 3, new DateTime(2025, 9, 12, 10, 0, 0, 0, DateTimeKind.Utc), 1, "LINQ tutorial", "https://moodle.com/materials/linq-tutorial.pdf" },
                    { 4, new DateTime(2025, 9, 2, 8, 0, 0, 0, DateTimeKind.Utc), 2, "SQL osnove", "https://moodle.com/materials/sql-basics.pdf" },
                    { 5, new DateTime(2025, 9, 9, 11, 0, 0, 0, DateTimeKind.Utc), 2, "Entity Framework Core", "https://moodle.com/materials/ef-core.pdf" },
                    { 6, new DateTime(2025, 9, 3, 8, 0, 0, 0, DateTimeKind.Utc), 3, "HTML & CSS", "https://moodle.com/materials/html-css.pdf" },
                    { 7, new DateTime(2025, 9, 10, 9, 0, 0, 0, DateTimeKind.Utc), 3, "JavaScript osnove", "https://moodle.com/materials/js-basics.pdf" },
                    { 8, new DateTime(2025, 9, 4, 10, 0, 0, 0, DateTimeKind.Utc), 4, "Složenost algoritama", "https://moodle.com/materials/algorithm-complexity.pdf" },
                    { 9, new DateTime(2025, 9, 6, 8, 0, 0, 0, DateTimeKind.Utc), 5, "TCP/IP protokoli", "https://moodle.com/materials/tcp-ip.pdf" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "messages",
                columns: new[] { "id", "chat_room_id", "content", "sender_id", "sent_at", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Poštovani profesore, imam pitanje vezano uz zadaću.", 5, new DateTime(2025, 9, 10, 14, 0, 0, 0, DateTimeKind.Utc), null },
                    { 2, 1, "Naravno, slobodno pitajte.", 2, new DateTime(2025, 9, 10, 14, 15, 0, 0, DateTimeKind.Utc), null },
                    { 3, 1, "Može li se zadaća predati dan kasnije?", 5, new DateTime(2025, 9, 10, 14, 20, 0, 0, DateTimeKind.Utc), null },
                    { 4, 1, "Da, ali uz umanjenje bodova od 10%.", 2, new DateTime(2025, 9, 10, 14, 30, 0, 0, DateTimeKind.Utc), null },
                    { 5, 2, "Profesorice, kada su konzultacije?", 6, new DateTime(2025, 9, 11, 10, 0, 0, 0, DateTimeKind.Utc), null },
                    { 6, 2, "Konzultacije su utorkom od 12-14h.", 3, new DateTime(2025, 9, 11, 10, 30, 0, 0, DateTimeKind.Utc), null },
                    { 7, 3, "Hej, jesi riješio zadaću iz C#?", 5, new DateTime(2025, 9, 12, 18, 0, 0, 0, DateTimeKind.Utc), null },
                    { 8, 3, "Radim na tome, treći zadatak mi nije jasan.", 6, new DateTime(2025, 9, 12, 18, 10, 0, 0, DateTimeKind.Utc), null },
                    { 9, 3, "Mogu ti pomoći, javi se na Discordu.", 5, new DateTime(2025, 9, 12, 18, 15, 0, 0, DateTimeKind.Utc), null },
                    { 10, 4, "Profesore, nedostaje mi materijal s prošlog predavanja.", 7, new DateTime(2025, 9, 13, 9, 0, 0, 0, DateTimeKind.Utc), null },
                    { 11, 4, "Uploadat ću ga danas do kraja dana.", 4, new DateTime(2025, 9, 13, 9, 45, 0, 0, DateTimeKind.Utc), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "course_students",
                keyColumns: new[] { "CourseId", "UserId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "messages",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "chatrooms",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "chatrooms",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "chatrooms",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "chatrooms",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 7);
        }
    }
}
