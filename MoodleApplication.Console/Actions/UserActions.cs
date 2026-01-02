using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Application.Users.User;

namespace MoodleApplication.Console.Actions
{
    public class UserActions
    {
        private readonly CreateUserRequestHandler _createUserRequestHandler;
        private readonly GetUserRequestHandler _getUserRequestHandler;
        public UserActions(CreateUserRequestHandler createUserRequestHandler, GetUserRequestHandler getUserRequestHandler)
        {
            _createUserRequestHandler = createUserRequestHandler;
            _getUserRequestHandler = getUserRequestHandler;
        }
        public async Task<bool> RegisterUser(string name, DateOnly dateofbirth,string email,string password) {
            var createUserRequest = new CreateUserRequest
            {
                Name = name,
                Email = email,
                PasswordHash = password,
                DateOfBirth = dateofbirth
            };
            var result = await _createUserRequestHandler.ProcessActiveRequestAsnync(createUserRequest);
            return result.HasError;
            
        }
        public async Task<(bool IsSuccess, int UserId, string Role)> LoginUser(string email, string password)
        {
            var getUserRequest = new GetUserRequest
            {
                Email = email,
                PasswordHash = password
            };
            var result = await _getUserRequestHandler.ProcessActiveRequestAsnync(getUserRequest);
            if (result.HasError || result.Value is null)
            {
                return (false, 0, string.Empty);
            }
            return (true, result.Value.UserId, result.Value.Role.ToString());
        }
    }
}
