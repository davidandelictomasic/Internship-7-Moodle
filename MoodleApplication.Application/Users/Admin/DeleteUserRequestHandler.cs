using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Admin
{
    public class DeleteUserRequest
    {
        public int UserId { get; set; }
    }

    public class DeleteUserRequestHandler : RequestHandler<DeleteUserRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public DeleteUserRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<SuccessPostResponse>> HandleRequest(
            DeleteUserRequest request,
            Common.Model.Result<SuccessPostResponse> result)
        {
            await _unitOfWork.UserRepository.DeleteUser(request.UserId);

            result.SetResult(new SuccessPostResponse(request.UserId));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
