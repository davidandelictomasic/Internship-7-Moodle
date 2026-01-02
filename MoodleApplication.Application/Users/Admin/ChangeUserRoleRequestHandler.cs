using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Admin
{
    public class ChangeUserRoleRequest
    {
        public int UserId { get; set; }
        public UserRole NewRole { get; set; }
    }

    public class ChangeUserRoleRequestHandler : RequestHandler<ChangeUserRoleRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public ChangeUserRoleRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<SuccessPostResponse>> HandleRequest(
            ChangeUserRoleRequest request,
            Common.Model.Result<SuccessPostResponse> result)
        {
            await _unitOfWork.UserRepository.ChangeRole(request.UserId, request.NewRole);

            result.SetResult(new SuccessPostResponse(request.UserId));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
