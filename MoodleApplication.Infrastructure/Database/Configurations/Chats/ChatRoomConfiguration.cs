using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleApplication.Domain.Entities.Chats;

namespace MoodleApplication.Infrastructure.Database.Configurations.Chats
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.ToTable("chatrooms");

            
            builder.HasKey(cr => cr.Id);

            builder.Property(cr => cr.Id)
                   .HasColumnName("id");

          
            builder.Property(cr => cr.FirstUserId)
                   .HasColumnName("first_user_id")
                   .IsRequired();

            
            builder.Property(cr => cr.SecondUserId)
                   .HasColumnName("second_user_id")
                   .IsRequired();

            
            builder.HasOne(cr => cr.FirstUser)
                   .WithMany()
                   .HasForeignKey(cr => cr.FirstUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cr => cr.SecondUser)
                   .WithMany()
                   .HasForeignKey(cr => cr.SecondUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(cr => cr.Messages)
                   .WithOne(m => m.ChatRoom)
                   .HasForeignKey(m => m.ChatRoomId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
