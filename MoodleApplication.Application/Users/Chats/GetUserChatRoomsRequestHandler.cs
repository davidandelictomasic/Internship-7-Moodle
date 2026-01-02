using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Chats
{
    public class ChatRoomResponse
    {
        public int ChatRoomId { get; set; }
        public int OtherUserId { get; set; }
        public string? OtherUserName { get; set; }
    }

    public class GetUserChatRoomsRequest
    {
        public int UserId { get; set; }
    }

    public class GetUserChatRoomsRequestHandler : RequestHandler<GetUserChatRoomsRequest, GetAllResponse<ChatRoomResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetUserChatRoomsRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<ChatRoomResponse>>> HandleRequest(
            GetUserChatRoomsRequest request,
            Common.Model.Result<GetAllResponse<ChatRoomResponse>> result)
        {
            var chatRooms = await _unitOfWork.ChatRoomRepository.GetUserChatRooms(request.UserId);

            var chatRoomResponses = chatRooms.Select(cr => new ChatRoomResponse
            {
                ChatRoomId = cr.Id,
                OtherUserId = cr.FirstUserId == request.UserId ? cr.SecondUserId : cr.FirstUserId,
                OtherUserName = cr.FirstUserId == request.UserId ? cr.SecondUser?.Name : cr.FirstUser?.Name
            });

            result.SetResult(new GetAllResponse<ChatRoomResponse>(chatRoomResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
