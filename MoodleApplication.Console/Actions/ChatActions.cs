using MoodleApplication.Application.Users.Chats;
using MoodleApplication.Application.Users.User;

namespace MoodleApplication.Console.Actions
{
    public class ChatActions
    {
        private readonly GetUserChatRoomsRequestHandler _getUserChatRoomsRequestHandler;
        private readonly GetChatMessagesRequestHandler _getChatMessagesRequestHandler;
        private readonly SendMessageRequestHandler _sendMessageRequestHandler;

        public ChatActions(
            
            GetUserChatRoomsRequestHandler getUserChatRoomsRequestHandler,
            GetChatMessagesRequestHandler getChatMessagesRequestHandler,
            SendMessageRequestHandler sendMessageRequestHandler)
        {
            _getUserChatRoomsRequestHandler = getUserChatRoomsRequestHandler;
            _getChatMessagesRequestHandler = getChatMessagesRequestHandler;
            _sendMessageRequestHandler = sendMessageRequestHandler;
        }

        

        public async Task<IEnumerable<ChatRoomResponse>> GetUserChatRooms(int userId)
        {
            var request = new GetUserChatRoomsRequest { UserId = userId };
            var result = await _getUserChatRoomsRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }

        public async Task<IEnumerable<MessageResponse>> GetChatMessages(int chatRoomId)
        {
            var request = new GetChatMessagesRequest { ChatRoomId = chatRoomId };
            var result = await _getChatMessagesRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }

        public async Task<bool> SendMessage(int senderId, int receiverId, string content)
        {
            var request = new SendMessageRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };

            var result = await _sendMessageRequestHandler.ProcessActiveRequestAsnync(request);

            return result.Value?.Id != null;
        }
    }
}
