using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Admin
{
    public class UpdateUserEmailRequest
    {
        public int UserId { get; set; }
        public string NewEmail { get; set; } = string.Empty;
    }

    public class UpdateUserEmailResponse
    {
        public int? UserId { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class UpdateUserEmailRequestHandler : RequestHandler<UpdateUserEmailRequest, UpdateUserEmailResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public UpdateUserEmailRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<UpdateUserEmailResponse>> HandleRequest(
            UpdateUserEmailRequest request,
            Common.Model.Result<UpdateUserEmailResponse> result)
        {
            var existingUser = await _unitOfWork.UserRepository.GetByEmail(request.NewEmail);
            if (existingUser != null && existingUser.Id != request.UserId)
            {
                result.SetResult(new UpdateUserEmailResponse
                {
                    UserId = null,
                    Success = false,
                    ErrorMessage = "Email already exists."
                });
                return result;
            }

            await _unitOfWork.UserRepository.UpdateEmail(request.UserId, request.NewEmail);

            result.SetResult(new UpdateUserEmailResponse
            {
                UserId = request.UserId,
                Success = true
            });

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
