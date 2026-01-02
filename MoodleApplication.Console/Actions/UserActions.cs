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
        public UserActions(CreateUserRequestHandler createUserRequestHandler)
        {
            _createUserRequestHandler = createUserRequestHandler;
        }
        public async Task<bool> RegisterUser(string name,string email,string password,DateOnly dateofbirth) {
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
    }
}
