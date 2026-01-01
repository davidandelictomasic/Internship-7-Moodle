
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Chats
{
    public class Message
    {
        public int Id { get;  set; }

        public int ChatRoomId { get;  set; }
        public ChatRoom ChatRoom { get;  set; } = null!;

        public int SenderId { get;  set; }
        public User Sender { get;  set; } = null!;

        public string Content { get;  set; } = null!;
        public DateTime SentAt { get;  set; } = DateTime.UtcNow;


        //public Message(ChatRoom chatRoom, User sender, string content)
        //{
        //    ChatRoom = chatRoom;
        //    ChatRoomId = chatRoom.Id;
        //    Sender = sender;
        //    SenderId = sender.Id;
        //    Content = content;
        //}
    }
}
