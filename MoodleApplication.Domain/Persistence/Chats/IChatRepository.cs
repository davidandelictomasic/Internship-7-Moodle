using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Persistence.Common;

namespace MoodleApplication.Domain.Persistence.Chats
{
    public interface IChatRoomRepository : IRepository<ChatRoom, int>
    {
        Task<ChatRoom> CreateChatRoom(int firstUserId, int secondUserId);
        Task<IEnumerable<ChatRoom>> GetUserChatRooms(int userId);
        Task<ChatRoom?> GetChatRoomBetweenUsers(int firstUserId, int secondUserId);

        
        Task<IEnumerable<Message>> GetMessagesForChatRoom(int chatRoomId);
        Task<Message> SendMessage(int chatRoomId, Message message);
    }

}
