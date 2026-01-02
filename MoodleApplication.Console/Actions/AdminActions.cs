using MoodleApplication.Application.Users.Admin;
using MoodleApplication.Domain.Enumumerations.Users;

namespace MoodleApplication.Console.Actions
{
    public class AdminActions
    {
        private readonly GetUsersByRoleRequestHandler _getUsersByRoleRequestHandler;
        private readonly DeleteUserRequestHandler _deleteUserRequestHandler;
        private readonly UpdateUserEmailRequestHandler _updateUserEmailRequestHandler;
        private readonly ChangeUserRoleRequestHandler _changeUserRoleRequestHandler;

        public AdminActions(
            GetUsersByRoleRequestHandler getUsersByRoleRequestHandler,
            DeleteUserRequestHandler deleteUserRequestHandler,
            UpdateUserEmailRequestHandler updateUserEmailRequestHandler,
            ChangeUserRoleRequestHandler changeUserRoleRequestHandler)
        {
            _getUsersByRoleRequestHandler = getUsersByRoleRequestHandler;
            _deleteUserRequestHandler = deleteUserRequestHandler;
            _updateUserEmailRequestHandler = updateUserEmailRequestHandler;
            _changeUserRoleRequestHandler = changeUserRoleRequestHandler;
        }

        public async Task<IEnumerable<UserListResponse>> GetUsersByRole(UserRole role)
        {
            var request = new GetUsersByRoleRequest { Role = role };
            var result = await _getUsersByRoleRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var request = new DeleteUserRequest { UserId = userId };
            var result = await _deleteUserRequestHandler.ProcessActiveRequestAsnync(request);

            return result.Value?.Id != null;
        }

        public async Task<(bool Success, string? Error)> UpdateUserEmail(int userId, string newEmail)
        {
            var request = new UpdateUserEmailRequest { UserId = userId, NewEmail = newEmail };
            var result = await _updateUserEmailRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
            {
                return (false, "Unknown error");
            }

            return (result.Value.Success, result.Value.ErrorMessage);
        }

        public async Task<bool> ChangeUserRole(int userId, UserRole newRole)
        {
            var request = new ChangeUserRoleRequest { UserId = userId, NewRole = newRole };
            var result = await _changeUserRoleRequestHandler.ProcessActiveRequestAsnync(request);

            return result.Value?.Id != null;
        }
    }
}
