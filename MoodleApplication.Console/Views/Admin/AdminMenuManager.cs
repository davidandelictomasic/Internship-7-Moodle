using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;
using MoodleApplication.Console.Views.Chats;
using MoodleApplication.Console.Views.Common;
using MoodleApplication.Domain.Enumumerations.Users;

namespace MoodleApplication.Console.Views.Admin
{
    public class AdminMenuManager
    {
        private readonly AdminActions _adminActions;
        private readonly ChatMenuManager _chatMenu;

        public AdminMenuManager(AdminActions adminActions, ChatMenuManager chatMenu)
        {
            _adminActions = adminActions;
            _chatMenu = chatMenu;
        }

        public async Task ShowAdminMenu(int userId)
        {

            bool exitRequested = false;

            var adminMenuOptions = MenuOptions.CreateAdminMenuOptions(this, userId);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - ADMIN MENU", adminMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (adminMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await adminMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowPrivateChatMenu(int userId)
        {
            await _chatMenu.ShowPrivateChatMenu(userId);
        }

        public async Task ShowEditUsersMenu()
        {

            bool exitRequested = false;

            var editUsersMenuOptions = MenuOptions.CreateEditUsersMenuOptions(this);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - EDIT USERS", editUsersMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (editUsersMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await editUsersMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowDeleteUserRoleSelectMenu()
        {
            await ShowRoleSelectMenu("delete");
        }

        public async Task ShowEditEmailRoleSelectMenu()
        {
            await ShowRoleSelectMenu("email");
        }

        public async Task ShowChangeRoleSelectMenu()
        {
            await ShowRoleSelectMenu("role");
        }

        private async Task ShowRoleSelectMenu(string action)
        {

            bool exitRequested = false;

            var roleSelectMenuOptions = MenuOptions.CreateRoleSelectMenuOptions(this, action);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - SELECT USER TYPE", roleSelectMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (roleSelectMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await roleSelectMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                    
                }
            }
        }

        public async Task ShowUserListForAction(UserRole role, string action)
        {
            System.Console.Clear();

            var users = await _adminActions.GetUsersByRole(role);

            if (!users.Any())
            {
                Writer.WriteMessage($"No {role}s found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var userListMenuOptions = MenuOptions.CreateUserListForActionMenuOptions(this, users, action, role);
            while (!exitRequested)
            {
                System.Console.Clear();

                var title = role == UserRole.Student ? "STUDENT LIST" : "PROFESSOR LIST";
                Writer.DisplayMenu($"Moodle - {title}", userListMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (userListMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await userListMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ExecuteUserAction(int userId, string userName, string userEmail, string action, UserRole currentRole)
        {
            System.Console.Clear();

            switch (action)
            {
                case "delete":
                    await HandleDeleteUser(userId, userName);
                    break;
                case "email":
                    await HandleEditEmail(userId, userName, userEmail);
                    break;
                case "role":
                    await HandleChangeRole(userId, userName, currentRole);
                    break;
            }
        }

        private async Task HandleDeleteUser(int userId, string userName)
        {
            Writer.WriteMessage($"=== DELETE USER: {userName} ===\n");
            Writer.WriteMessage("Are you sure you want to delete this user?");
            Writer.WriteMessage("This will also delete all their messages and course enrollments.");
            var confirm = Reader.ReadString("Type 'YES' to confirm: ");

            if (confirm.Equals("YES", StringComparison.Ordinal))
            {
                var success = await _adminActions.DeleteUser(userId);
                if (success)
                {
                    Writer.WriteMessage("User deleted successfully.");
                }
                else
                {
                    Writer.WriteMessage("Error deleting user.");
                }
            }
            else
            {
                Writer.WriteMessage("Deletion cancelled.");
            }

            Writer.WaitForKey();
        }

        private async Task HandleEditEmail(int userId, string userName, string currentEmail)
        {
            Writer.WriteMessage($"=== EDIT EMAIL: {userName} ===\n");
            Writer.WriteMessage($"Current email: {currentEmail}");
            var newEmail = Reader.ReadEmail("Enter new email: ");

            var (success, error) = await _adminActions.UpdateUserEmail(userId, newEmail);
            if (success)
            {
                Writer.WriteMessage("Email updated successfully.");
            }
            else
            {
                Writer.WriteMessage($"Error updating email: {error}");
            }

            Writer.WaitForKey();
        }

        private async Task HandleChangeRole(int userId, string userName, UserRole currentRole)
        {
            var newRole = currentRole == UserRole.Student ? UserRole.Professor : UserRole.Student;
            var actionText = currentRole == UserRole.Student ? "promote to Professor" : "demote to Student";

            Writer.WriteMessage($"=== CHANGE ROLE: {userName} ===\n");
            Writer.WriteMessage($"Current role: {currentRole}");
            Writer.WriteMessage($"Do you want to {actionText}?");
            var confirm = Reader.ReadString("Type 'YES' to confirm: ");

            if (confirm.Equals("YES", StringComparison.Ordinal))
            {
                var success = await _adminActions.ChangeUserRole(userId, newRole);
                if (success)
                {
                    Writer.WriteMessage($"Role changed to {newRole} successfully.");
                }
                else
                {
                    Writer.WriteMessage("Error changing role.");
                }
            }
            else
            {
                Writer.WriteMessage("Role change cancelled.");
            }

            Writer.WaitForKey();
        }
    }
}
