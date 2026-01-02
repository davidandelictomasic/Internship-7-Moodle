using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodleApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chatrooms_users_first_user_id",
                schema: "public",
                table: "chatrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_chatrooms_users_second_user_id",
                schema: "public",
                table: "chatrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_sender_id",
                schema: "public",
                table: "messages");

            migrationBuilder.AddForeignKey(
                name: "FK_chatrooms_users_first_user_id",
                schema: "public",
                table: "chatrooms",
                column: "first_user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chatrooms_users_second_user_id",
                schema: "public",
                table: "chatrooms",
                column: "second_user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_sender_id",
                schema: "public",
                table: "messages",
                column: "sender_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chatrooms_users_first_user_id",
                schema: "public",
                table: "chatrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_chatrooms_users_second_user_id",
                schema: "public",
                table: "chatrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_sender_id",
                schema: "public",
                table: "messages");

            migrationBuilder.AddForeignKey(
                name: "FK_chatrooms_users_first_user_id",
                schema: "public",
                table: "chatrooms",
                column: "first_user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chatrooms_users_second_user_id",
                schema: "public",
                table: "chatrooms",
                column: "second_user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_sender_id",
                schema: "public",
                table: "messages",
                column: "sender_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
