using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Persistence.Chats;
using MoodleApplication.Infrastructure.Database;
using MoodleApplication.Infrastructure.Persistence.Common;

namespace MoodleApplication.Infrastructure.Persistence.Chats
{
    public class ChatRepository : Repository<ChatRoom, int>, IChatRoomRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<ChatRoom> CreateChatRoom(int firstUserId, int secondUserId)
        {
            throw new NotImplementedException();
        }

        public Task<ChatRoom?> GetChatRoomBetweenUsers(int firstUserId, int secondUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetMessagesForChatRoom(int chatRoomId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatRoom>> GetUserChatRooms(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendMessage(int chatRoomId, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
