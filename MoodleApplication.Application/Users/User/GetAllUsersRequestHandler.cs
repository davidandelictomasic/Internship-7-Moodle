using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.User
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class GetAllUsersRequest
    {
        public int CurrentUserId { get; set; }
    }

    public class GetAllUsersRequestHandler : RequestHandler<GetAllUsersRequest, GetAllResponse<UserResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetAllUsersRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<UserResponse>>> HandleRequest(
            GetAllUsersRequest request,
            Common.Model.Result<GetAllResponse<UserResponse>> result)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers();

            var userResponses = users
                .Where(u => u.Id != request.CurrentUserId)
                .Select(u => new UserResponse
                {
                    UserId = u.Id,
                    Name = u.Name,
                    Email = u.Email
                });

            result.SetResult(new GetAllResponse<UserResponse>(userResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
