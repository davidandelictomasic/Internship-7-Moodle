using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Admin
{
    public class UserListResponse
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
    }

    public class GetUsersByRoleRequest
    {
        public UserRole Role { get; set; }
    }

    public class GetUsersByRoleRequestHandler : RequestHandler<GetUsersByRoleRequest, GetAllResponse<UserListResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetUsersByRoleRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<UserListResponse>>> HandleRequest(
            GetUsersByRoleRequest request,
            Common.Model.Result<GetAllResponse<UserListResponse>> result)
        {
            var users = await _unitOfWork.UserRepository.GetUsersByRole(request.Role);

            var userResponses = users.Select(u => new UserListResponse
            {
                UserId = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });

            result.SetResult(new GetAllResponse<UserListResponse>(userResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
