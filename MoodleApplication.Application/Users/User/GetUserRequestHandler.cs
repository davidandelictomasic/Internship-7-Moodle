using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Validation;
using MoodleApplication.Domain.Common.Validation.ValidationItems;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.User
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
    public class GetUserRequest
    {
        public required string? Email { get; set; }
        public string? PasswordHash { get; set; }
        
    }
    public class GetUserRequestHandler : RequestHandler<GetUserRequest, LoginResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GetUserRequestHandler(IUserUnitOfWork userUnitOfWork)
        {
            _unitOfWork = userUnitOfWork;
        }

        protected async override Task<Result<LoginResponse>> HandleRequest(GetUserRequest request, Result<LoginResponse> result)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email!);

            if (user is null)
            {
                
                return result;
            }

            if (user.PasswordHash != request.PasswordHash)
            {                
                return result;
            }

            result.SetResult(new LoginResponse
            {
                UserId = user.Id,
                Role = user.Role
            });

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
