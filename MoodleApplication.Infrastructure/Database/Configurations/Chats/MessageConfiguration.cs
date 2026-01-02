using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleApplication.Domain.Entities.Chats;

namespace MoodleApplication.Infrastructure.Database.Configurations.Chats
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasColumnName("id");

            builder.Property(m => m.ChatRoomId)
                   .HasColumnName("chat_room_id")
                   .IsRequired();

            builder.Property(m => m.SenderId)
                   .HasColumnName("sender_id")
                   .IsRequired();

            builder.Property(m => m.Content)
                   .HasColumnName("content")
                   .IsRequired();

            builder.Property(m => m.SentAt)
                   .HasColumnName("sent_at")
                   .IsRequired();
        }
    }
}
