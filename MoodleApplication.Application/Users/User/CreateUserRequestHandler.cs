using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.User
{
    public class CreateUserRequest
    {
        public required string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public required string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
    public class CreateUserRequestHandler : RequestHandler<CreateUserRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public CreateUserRequestHandler(IUserUnitOfWork userUnitOfWork)
        {
            _unitOfWork = userUnitOfWork;
        }

        protected async override Task<Result<SuccessPostResponse>> HandleRequest(CreateUserRequest request, Result<SuccessPostResponse> result)
        {
            var user = new Domain.Entities.Users.User           
            {
                Email = request.Email!,
                PasswordHash = request.PasswordHash!,
                Name = request.Name,
                DateOfBirth = request.DateOfBirth
            };

            var validationResult = await user.Create(_unitOfWork.UserRepository);
            result.SetValidationResult(validationResult.ValidationResult);
            if (result.HasError)
                return result;

            await _unitOfWork.SaveAsync();
            result.SetResult(new SuccessPostResponse(user.Id));
            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
