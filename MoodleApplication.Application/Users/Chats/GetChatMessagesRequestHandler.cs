using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Chats
{
    public class MessageResponse
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
    }

    public class GetChatMessagesRequest
    {
        public int ChatRoomId { get; set; }
    }

    public class GetChatMessagesRequestHandler : RequestHandler<GetChatMessagesRequest, GetAllResponse<MessageResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetChatMessagesRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<MessageResponse>>> HandleRequest(
            GetChatMessagesRequest request,
            Common.Model.Result<GetAllResponse<MessageResponse>> result)
        {
            var messages = await _unitOfWork.ChatRoomRepository.GetMessagesForChatRoom(request.ChatRoomId);

            var messageResponses = messages.Select(m => new MessageResponse
            {
                MessageId = m.Id,
                SenderId = m.SenderId,
                SenderName = m.Sender?.Name,
                Content = m.Content,
                SentAt = m.SentAt
            });

            result.SetResult(new GetAllResponse<MessageResponse>(messageResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
