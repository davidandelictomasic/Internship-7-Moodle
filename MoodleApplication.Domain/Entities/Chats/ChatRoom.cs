
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Chats
{
   
        public class ChatRoom
        {
            public int Id { get;  set; }

            public int FirstUserId { get;  set; }
            public User FirstUser { get;  set; } = null!;

            public int SecondUserId { get;  set; }
            public User SecondUser { get;  set; } = null!;

            public ICollection<Message> Messages { get;  set; } = [];


            //public ChatRoom(User firstUser, User secondUser)
            //{
            //    FirstUser = firstUser;
            //    FirstUserId = firstUser.Id;
            //    SecondUser = secondUser;
            //    SecondUserId = secondUser.Id;
            //}
        }
    
}
