
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Chats
{
    public class Message
    {
        public int Id { get; private set; }

        public int ChatRoomId { get; private set; }
        public ChatRoom ChatRoom { get; private set; } = null!;

        public int SenderId { get; private set; }
        public User Sender { get; private set; } = null!;

        public string Content { get; private set; } = null!;
        public DateTime SentAt { get; private set; } = DateTime.UtcNow;


        public Message(ChatRoom chatRoom, User sender, string content)
        {
            ChatRoom = chatRoom;
            ChatRoomId = chatRoom.Id;
            Sender = sender;
            SenderId = sender.Id;
            Content = content;
        }
    }
}
