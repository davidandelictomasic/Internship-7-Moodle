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
        private readonly GetUserCoursesRequestHandler _getUserCoursesRequestHandler;      
        private readonly GetAllUsersRequestHandler _getAllUsersRequestHandler;
        private readonly GetTeachingCoursesRequestHandler _getTeachingCoursesRequestHandler;

        public UserActions(
            CreateUserRequestHandler createUserRequestHandler, 
            GetUserRequestHandler getUserRequestHandler, 
            GetUserCoursesRequestHandler getUserCoursesRequestHandler, 
            GetAllUsersRequestHandler getAllUsersRequestHandler,
            GetTeachingCoursesRequestHandler getTeachingCoursesRequestHandler)
        {
            _createUserRequestHandler = createUserRequestHandler;
            _getUserRequestHandler = getUserRequestHandler;
            _getUserCoursesRequestHandler = getUserCoursesRequestHandler;
            _getAllUsersRequestHandler = getAllUsersRequestHandler;
            _getTeachingCoursesRequestHandler = getTeachingCoursesRequestHandler;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers(int currentUserId)
        {
            var request = new GetAllUsersRequest { CurrentUserId = currentUserId };
            var result = await _getAllUsersRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }

        public async Task<int?> RegisterUser(string name, DateOnly dateofbirth, string email, string password)
        {
            var createUserRequest = new CreateUserRequest
            {
                Name = name,
                Email = email,
                PasswordHash = password,
                DateOfBirth = dateofbirth
            };
            var result = await _createUserRequestHandler.ProcessActiveRequestAsnync(createUserRequest);            
            return result.Value.Id;
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

        public async Task<IEnumerable<CoursesResponse>> GetUserCourses(int userId)
        {
            var getUserCoursesRequest = new GetUserCoursesRequest
            {
                Id = userId
            };
            var result = await _getUserCoursesRequestHandler.ProcessActiveRequestAsnync(getUserCoursesRequest);

            if (result.Value == null)
                return [];

            return result.Value.Values.Select(b => new CoursesResponse
            {
                CourseId = b.CourseId,
                CourseName = b.CourseName
            });
        }

        public async Task<IEnumerable<TeachingCourseResponse>> GetTeachingCourses(int professorId)
        {
            var request = new GetTeachingCoursesRequest { ProfessorId = professorId };
            var result = await _getTeachingCoursesRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }
    }
}
