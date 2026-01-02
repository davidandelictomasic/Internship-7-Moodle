using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Chats
{
    public class SendMessageRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
    }

    public class SendMessageRequestHandler : RequestHandler<SendMessageRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public SendMessageRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<SuccessPostResponse>> HandleRequest(
            SendMessageRequest request,
            Common.Model.Result<SuccessPostResponse> result)
        {
            var chatRoom = await _unitOfWork.ChatRoomRepository.GetChatRoomBetweenUsers(request.SenderId, request.ReceiverId);

            if (chatRoom == null)
            {
                chatRoom = await _unitOfWork.ChatRoomRepository.CreateChatRoom(request.SenderId, request.ReceiverId);
            }

            var message = new Message
            {
                SenderId = request.SenderId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            var sentMessage = await _unitOfWork.ChatRoomRepository.SendMessage(chatRoom.Id, message);

            result.SetResult(new SuccessPostResponse(sentMessage.Id));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
