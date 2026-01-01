
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Chats
{
   
        public class ChatRoom
        {
            public int Id { get; private set; }

            public int FirstUserId { get; private set; }
            public User FirstUser { get; private set; } = null!;

            public int SecondUserId { get; private set; }
            public User SecondUser { get; private set; } = null!;

            public ICollection<Message> Messages { get; private set; } = [];


            //public ChatRoom(User firstUser, User secondUser)
            //{
            //    FirstUser = firstUser;
            //    FirstUserId = firstUser.Id;
            //    SecondUser = secondUser;
            //    SecondUserId = secondUser.Id;
            //}
        }
    
}
