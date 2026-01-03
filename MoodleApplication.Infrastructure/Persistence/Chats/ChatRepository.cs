using Microsoft.EntityFrameworkCore;
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

        public async Task<ChatRoom> CreateChatRoom(int firstUserId, int secondUserId)
        {
            var chatRoom = new ChatRoom
            {
                FirstUserId = firstUserId,
                SecondUserId = secondUserId
            };

            await _dbContext.ChatRooms.AddAsync(chatRoom);
            await _dbContext.SaveChangesAsync();

            return chatRoom;
        }

        public async Task<ChatRoom?> GetChatRoomBetweenUsers(int firstUserId, int secondUserId)
        {
            return await _dbContext.ChatRooms
                .Include(c => c.FirstUser)
                .Include(c => c.SecondUser)
                .FirstOrDefaultAsync(c =>
                    (c.FirstUserId == firstUserId && c.SecondUserId == secondUserId) ||
                    (c.FirstUserId == secondUserId && c.SecondUserId == firstUserId));
        }

        public async Task<IEnumerable<Message>> GetMessagesForChatRoom(int chatRoomId)
        {
            return await _dbContext.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatRoom>> GetUserChatRooms(int userId)
        {
            return await _dbContext.ChatRooms
                .Include(c => c.FirstUser)
                .Include(c => c.SecondUser)
                .Include(c => c.Messages)
                .Where(c => c.FirstUserId == userId || c.SecondUserId == userId)
                .OrderByDescending(c => c.Messages.Max(m => m.SentAt))
                .ToListAsync();
        }

        public async Task<Message> SendMessage(int chatRoomId, Message message)
        {
            message.ChatRoomId = chatRoomId;
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return message;
        }
    }
}
