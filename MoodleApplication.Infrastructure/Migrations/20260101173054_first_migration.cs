using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoodleApplication.Infrastructure.Migrations
{
    
    public partial class first_migration : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chatrooms",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_user_id = table.Column<int>(type: "integer", nullable: false),
                    second_user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatrooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_chatrooms_users_first_user_id",
                        column: x => x.first_user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chatrooms_users_second_user_id",
                        column: x => x.second_user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    professor_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_users_professor_id",
                        column: x => x.professor_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chat_room_id = table.Column<int>(type: "integer", nullable: false),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_messages_chatrooms_chat_room_id",
                        column: x => x.chat_room_id,
                        principalSchema: "public",
                        principalTable: "chatrooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_messages_users_sender_id",
                        column: x => x.sender_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "announcements",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    professor_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_announcements", x => x.id);
                    table.ForeignKey(
                        name: "FK_announcements_courses_course_id",
                        column: x => x.course_id,
                        principalSchema: "public",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_announcements_users_professor_id",
                        column: x => x.professor_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_students",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_students", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_course_students_courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "public",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_students_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materials", x => x.id);
                    table.ForeignKey(
                        name: "FK_materials_courses_course_id",
                        column: x => x.course_id,
                        principalSchema: "public",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_announcements_course_id",
                schema: "public",
                table: "announcements",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_announcements_professor_id",
                schema: "public",
                table: "announcements",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_chatrooms_first_user_id",
                schema: "public",
                table: "chatrooms",
                column: "first_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_chatrooms_second_user_id",
                schema: "public",
                table: "chatrooms",
                column: "second_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_students_CourseId",
                schema: "public",
                table: "course_students",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_professor_id",
                schema: "public",
                table: "courses",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_materials_course_id",
                schema: "public",
                table: "materials",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_chat_room_id",
                schema: "public",
                table: "messages",
                column: "chat_room_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_sender_id",
                schema: "public",
                table: "messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserId",
                schema: "public",
                table: "messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "announcements",
                schema: "public");

            migrationBuilder.DropTable(
                name: "course_students",
                schema: "public");

            migrationBuilder.DropTable(
                name: "materials",
                schema: "public");

            migrationBuilder.DropTable(
                name: "messages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "chatrooms",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
