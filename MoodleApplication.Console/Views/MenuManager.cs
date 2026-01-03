using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;
using MoodleApplication.Domain.Enumumerations.Users;
using Npgsql.Internal;

namespace MoodleApplication.Console.Views
{
    public class MenuManager
    {
        private readonly UserActions _userActions;
        private readonly CourseActions _courseActions;
        private readonly ChatActions _chatActions;
        private readonly AdminActions _adminActions;

        public MenuManager(
            UserActions userActions, 
            CourseActions courseActions, 
            ChatActions chatActions, 
            AdminActions adminActions)
        {
            _userActions = userActions;
            _courseActions = courseActions;
            _chatActions = chatActions;
            _adminActions = adminActions;
        }

        public async Task RunAsync()
        {
            System.Console.Clear();

            bool exitRequested = false;

            var mainMenuOptions = MenuOptions.CreateStartMenuOptions(this);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - START MENU", mainMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (mainMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await mainMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();



        }
        public async Task ShowStudentMenu(int userId)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var studentMenuOptions = MenuOptions.CreateStudentMenuOptions(this, userId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - STUDENT MENU", studentMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();



        }
        public async Task HandleUserLogin()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER LOGIN ===");

            var userEmail = Reader.ReadEmail("Email: ");
            var userPassword = Reader.ReadString("Password: ");
            var loginReult = await _userActions.LoginUser(userEmail, userPassword);
            if(loginReult.IsSuccess)
            {
                Writer.WriteMessage("Login successful.");
                Writer.WaitForKey();
                if (loginReult.Role == "Student")
                {
                    await ShowStudentMenu(loginReult.UserId);
                }
                else if (loginReult.Role == "Admin")
                {
                    await ShowAdminMenu(loginReult.UserId);
                }
                else if (loginReult.Role == "Professor")
                {
                    await ShowProfessorMenu(loginReult.UserId);
                }
            }
            else
            {
                Writer.WriteMessage("Login failed. Please check your credentials.");
                Writer.WaitForKey();
            }
        }
        public async Task HandleUserRegister()
        {
            System.Console.Clear();
            Writer.WriteMessage("=== USER REGISTRATION ===");

            var userName = Reader.ReadString("Full Name: ");

            var userDob = Reader.ReadDateOfBirth("Date of Birth (YYYY-MM-DD): ");

            var userEmail = Reader.ReadEmail("Email: ");

            var userPassword = Reader.ReadString("Password: ");

            var captcha = Writer.GenerateCaptcha();

            Writer.WriteMessage($"CAPTCHA: {captcha}");
            Reader.ValidateCaptcha(captcha);
           if (await _userActions.RegisterUser(userName, userDob, userEmail, userPassword))
           {
               Writer.WriteMessage("User registered successfully.");    
                Writer.WaitForKey();

            }
           else
           {
               Writer.WriteMessage("User registration failed.");
           }
        }
        public async Task ShowUserCourses(int userId)
        {
            System.Console.Clear();

            var userCourses = await _userActions.GetUserCourses(userId);

            if (!userCourses.Any())
            {
                Writer.WriteMessage("No courses found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var coursesMenuOptions = MenuOptions.CreateStudentCoursesMenuOptions(this, userCourses);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MOJI KOLEGIJI", coursesMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (coursesMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await coursesMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }
       

        public async Task ShowSelectedCourseMenu(int courseId)
        {
            System.Console.Clear();
            bool exitRequested = false;

            var studentCourseMenuOptions = MenuOptions.CreateStudentCourseMenuOptions(this, courseId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MY COURSES", studentCourseMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentCourseMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentCourseMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }      

        public async Task ShowPrivateChatMenu(int userId)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var privateChatMenuOptions = MenuOptions.CreatePrivateChatMenuOptions(this, userId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - PRIVATE CHAT", privateChatMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (privateChatMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await privateChatMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowNewMessageMenu(int currentUserId)
        {
            System.Console.Clear();

            var users = await _userActions.GetAllUsers(currentUserId);

            if (!users.Any())
            {
                Writer.WriteMessage("No users found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var usersMenuOptions = MenuOptions.CreateUsersListMenuOptions(this, currentUserId, users);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - SEND MESSAGE TO USER", usersMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (usersMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await usersMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowSendMessageScreen(int senderId, int receiverId, string receiverName)
        {
            System.Console.Clear();
            Writer.WriteMessage($"=== NEW MESSAGE FOR: {receiverName} ===\n");

            var messageContent = Reader.ReadString("Input message: ");

            var success = await _chatActions.SendMessage(senderId, receiverId, messageContent);

            if (success)
            {
                Writer.WriteMessage("Message sent!");
            }
            else
            {
                Writer.WriteMessage("Error while sending.");
            }

            Writer.WaitForKey();
            System.Console.Clear();
        }

        public async Task ShowMyChatRoomsMenu(int userId)
        {
            System.Console.Clear();

            var chatRooms = await _chatActions.GetUserChatRooms(userId);

            if (!chatRooms.Any())
            {
                Writer.WriteMessage("No chats.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var chatRoomsMenuOptions = MenuOptions.CreateChatRoomsMenuOptions(this, userId, chatRooms);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MY CHATS", chatRoomsMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (chatRoomsMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await chatRoomsMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowChatScreen(int currentUserId, int chatRoomId, int otherUserId, string otherUserName)
        {
            bool exitChat = false;

            while (!exitChat)
            {
                System.Console.Clear();
                Writer.WriteMessage($"=== CHAT WITH: {otherUserName} ===");
                Writer.WriteMessage("(Type /exit to go back)\n");

                var messages = await _chatActions.GetChatMessages(chatRoomId);

                if (!messages.Any())
                {
                    Writer.WriteMessage("No messages in this chat.\n");
                }
                else
                {
                    foreach (var message in messages)
                    {
                        var senderLabel = message.SenderId == currentUserId ? "You" : message.SenderName;
                        Writer.WriteMessage($"[{message.SentAt:dd.MM.yyyy HH:mm}] {senderLabel}: {message.Content}");
                    }
                }

                Writer.WriteMessage("");
                var messageContent = Reader.ReadString("Input message: ");

                if (messageContent.Equals("/exit", StringComparison.OrdinalIgnoreCase))
                {
                    exitChat = true;
                }
                else
                {
                    await _chatActions.SendMessage(currentUserId, otherUserId, messageContent);
                }
            }
            System.Console.Clear();
        }


        public async Task ShowAdminMenu(int userId)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var adminMenuOptions = MenuOptions.CreateAdminMenuOptions(this, userId);
            while (!exitRequested)
            {
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
                }
            }
            System.Console.Clear();
        }

        public async Task ShowEditUsersMenu()
        {
            System.Console.Clear();

            bool exitRequested = false;

            var editUsersMenuOptions = MenuOptions.CreateEditUsersMenuOptions(this);
            while (!exitRequested)
            {
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
                }
            }
            System.Console.Clear();
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
            System.Console.Clear();

            bool exitRequested = false;

            var roleSelectMenuOptions = MenuOptions.CreateRoleSelectMenuOptions(this, action);
            while (!exitRequested)
            {
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
                }
            }
            System.Console.Clear();
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
                }
            }
            System.Console.Clear();
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
            var action = currentRole == UserRole.Student ? "promote to Professor" : "demote to Student";

            Writer.WriteMessage($"=== CHANGE ROLE: {userName} ===\n");
            Writer.WriteMessage($"Current role: {currentRole}");
            Writer.WriteMessage($"Do you want to {action}?");
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


        public async Task ShowProfessorMenu(int userId)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var professorMenuOptions = MenuOptions.CreateProfessorMenuOptions(this, userId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - PROFESSOR MENU", professorMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (professorMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await professorMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowProfessorCourses(int professorId)
        {
            System.Console.Clear();

            var courses = await _userActions.GetTeachingCourses(professorId);

            if (!courses.Any())
            {
                Writer.WriteMessage("No courses found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var coursesMenuOptions = MenuOptions.CreateProfessorCoursesMenuOptions(this, courses);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - MY COURSES", coursesMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (coursesMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await coursesMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowProfessorCourseScreen(int courseId, string courseName)
        {
            System.Console.Clear();

            bool exitRequested = false;

            var courseMenuOptions = MenuOptions.CreateProfessorCourseMenuOptions(this, courseId, courseName);
            while (!exitRequested)
            {
                Writer.DisplayMenu($"Moodle - {courseName.ToUpper()}", courseMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (courseMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await courseMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                }
            }
            System.Console.Clear();
        }

        public async Task ShowCourseStudents(int courseId, string courseName)
        {
            System.Console.Clear();
            Writer.WriteMessage($"=== STUDENTS - {courseName} ===\n");

            var students = await _courseActions.GetCourseStudents(courseId);

            if (!students.Any())
            {
                Writer.WriteMessage("No students enrolled in this course.");
            }
            else
            {
                int index = 1;
                foreach (var student in students)
                {
                    Writer.WriteMessage($"{index}. {student.StudentName} ({student.StudentEmail})");
                    index++;
                }
            }

            Writer.WaitForKey();
            System.Console.Clear();
        }

        public async Task ShowCourseAnnouncements(int courseId)
        {
            System.Console.Clear();
            Writer.WriteMessage("=== ANNOUNCEMENTS ===\n");

            var announcements = await _courseActions.GetCourseAnnouncements(courseId);

            if (!announcements.Any())
            {
                Writer.WriteMessage("No announcements found.");
            }
            else
            {
                foreach (var announcement in announcements)
                {
                    Writer.WriteMessage($"[{announcement.AnnouncementCreatedAt:dd.MM.yyyy HH:mm}] {announcement.AnnouncementTitle}");
                    Writer.WriteMessage($"   {announcement.AnnouncementContent}");
                    Writer.WriteMessage($"   By: {announcement.ProfessorName}\n");
                }
            }

            Writer.WaitForKey();
            System.Console.Clear();
        }

        public async Task ShowCourseMaterials(int courseId)
        {
            System.Console.Clear();
            Writer.WriteMessage("=== MATERIALS ===\n");

            var materials = await _courseActions.GetCourseMaterials(courseId);

            if (!materials.Any())
            {
                Writer.WriteMessage("No materials found.");
            }
            else
            {
                foreach (var material in materials)
                {
                    Writer.WriteMessage($"[{material.MaterialAddedAt:dd.MM.yyyy}] {material.MaterialName}");
                    Writer.WriteMessage($"   URL: {material.MaterialURL}\n");
                }
            }

            Writer.WaitForKey();
            System.Console.Clear();
        }
    }
}
