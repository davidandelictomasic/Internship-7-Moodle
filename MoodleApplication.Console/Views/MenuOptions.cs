using MoodleApplication.Application.Users.Admin;
using MoodleApplication.Application.Users.Chats;
using MoodleApplication.Application.Users.User;
using MoodleApplication.Domain.Enumumerations.Users;

namespace MoodleApplication.Console.Views
{
    public class MenuOptions
    {
        private readonly Dictionary<string, (string Description, Func<Task<bool>> Action)> _options;

        public MenuOptions()
        {
            _options = [];
        }

        public MenuOptions AddOption(string key, string description, Func<Task<bool>> action)
        {
            _options.Add(key, (description, action));
            return this;
        }

        public Dictionary<string, (string Description, Func<Task<bool>> Action)> Build()
        {
            return _options;
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStartMenuOptions(MenuManager menuManager)
        {
                       var menuOptions = new MenuOptions()
                .AddOption("1", "Login", async () =>
                {
                    await menuManager.HandleUserLogin();
                    return false;
                })
                .AddOption("2", "Register", async () =>
                {
                    await menuManager.HandleUserRegister();
                    return false;
                })
                .AddOption("3", "Exit", async () =>
                {
                    return true;
                });
            return menuOptions.Build();

        }
        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStudentMenuOptions(MenuManager menuManager, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "Private chat", async () =>
                {
                    await menuManager.ShowPrivateChatMenu(userId);
                    return false;
                })
                .AddOption("2", "My courses", async () => { 
                    await menuManager.ShowUserCourses(userId);
                    return false; 
                })
                .AddOption("3", "Log out", async () => true)
                .Build();
        }
        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStudentCoursesMenuOptions(MenuManager menuManager, IEnumerable<CoursesResponse> courses)
        {
            var menuOptions = new MenuOptions();
            var coursesList = courses.ToList();

            for (int i = 0; i < coursesList.Count; i++)
            {
                var course = coursesList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, course.CourseName ?? "Unnamed Course", async () =>
                {
                    await menuManager.ShowSelectedCourseMenu(course.CourseId);
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }
        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStudentCourseMenuOptions(MenuManager menuManager, int courseId)
        {
            return new MenuOptions()
                .AddOption("1", "Announcements", async () =>
                {
                    await menuManager.ShowCourseAnnouncements(courseId);
                    return false;
                })
                .AddOption("2", "Materials", async () => {
                    await menuManager.ShowCourseMaterials(courseId);
                    return false;
                })
                .AddOption("3", "Back", async () => true)
                .Build();
        }
        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreatePrivateChatMenuOptions(MenuManager menuManager, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "New message", async () =>
                {
                    await menuManager.ShowNewMessageMenu(userId);
                    return false;
                })
                .AddOption("2", "My chats", async () =>
                {
                    await menuManager.ShowMyChatRoomsMenu(userId);
                    return false;
                })
                .AddOption("3", "Back", async () => true)
                .Build();
        }
        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateUsersListMenuOptions(MenuManager menuManager, int currentUserId, IEnumerable<UserResponse> users)
        {
            var menuOptions = new MenuOptions();
            var usersList = users.ToList();

            for (int i = 0; i < usersList.Count; i++)
            {
                var user = usersList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, $"{user.Name} ({user.Email})", async () =>
                {
                    await menuManager.ShowSendMessageScreen(currentUserId, user.UserId, user.Name ?? "Unknown");
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateChatRoomsMenuOptions(MenuManager menuManager, int currentUserId, IEnumerable<ChatRoomResponse> chatRooms)
        {
            var menuOptions = new MenuOptions();
            var chatRoomsList = chatRooms.ToList();

            for (int i = 0; i < chatRoomsList.Count; i++)
            {
                var chatRoom = chatRoomsList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, chatRoom.OtherUserName ?? "Unknown User", async () =>
                {
                    await menuManager.ShowChatScreen(currentUserId, chatRoom.ChatRoomId, chatRoom.OtherUserId, chatRoom.OtherUserName ?? "Unknown");
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateAdminMenuOptions(MenuManager menuManager, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "Private chat", async () =>
                {
                    await menuManager.ShowPrivateChatMenu(userId);
                    return false;
                })
                .AddOption("2", "Edit Users", async () =>
                {
                    await menuManager.ShowEditUsersMenu();
                    return false;
                })
                .AddOption("3", "Log out", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateEditUsersMenuOptions(MenuManager menuManager)
        {
            return new MenuOptions()
                .AddOption("1", "Delete User", async () =>
                {
                    await menuManager.ShowDeleteUserRoleSelectMenu();
                    return false;
                })
                .AddOption("2", "Edit User Email", async () =>
                {
                    await menuManager.ShowEditEmailRoleSelectMenu();
                    return false;
                })
                .AddOption("3", "Change Role", async () =>
                {
                    await menuManager.ShowChangeRoleSelectMenu();
                    return false;
                })
                .AddOption("0", "Back", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateRoleSelectMenuOptions(MenuManager menuManager, string action)
        {
            return new MenuOptions()
                .AddOption("1", "Student List", async () =>
                {
                    await menuManager.ShowUserListForAction(UserRole.Student, action);
                    return false;
                })
                .AddOption("2", "Professor List", async () =>
                {
                    await menuManager.ShowUserListForAction(UserRole.Professor, action);
                    return false;
                })
                .AddOption("0", "Back", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateUserListForActionMenuOptions(
            MenuManager menuManager, 
            IEnumerable<UserListResponse> users, 
            string action,
            UserRole currentRole)
        {
            var menuOptions = new MenuOptions();
            var usersList = users.ToList();

            for (int i = 0; i < usersList.Count; i++)
            {
                var user = usersList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, $"{user.Name} ({user.Email})", async () =>
                {
                    await menuManager.ExecuteUserAction(user.UserId, user.Name ?? "Unknown", user.Email ?? "", action, currentRole);
                    return true; 
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateProfessorMenuOptions(MenuManager menuManager, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "Private chat", async () =>
                {
                    await menuManager.ShowPrivateChatMenu(userId);
                    return false;
                })
                .AddOption("2", "My courses", async () =>
                {
                    await menuManager.ShowProfessorCourses(userId);
                    return false;
                })
                .AddOption("3", "Log out", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateProfessorCoursesMenuOptions(
            MenuManager menuManager, 
            IEnumerable<TeachingCourseResponse> courses)
        {
            var menuOptions = new MenuOptions();
            var coursesList = courses.ToList();

            for (int i = 0; i < coursesList.Count; i++)
            {
                var course = coursesList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, course.CourseName ?? "Unnamed Course", async () =>
                {
                    await menuManager.ShowProfessorCourseScreen(course.CourseId, course.CourseName ?? "Course");
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateProfessorCourseMenuOptions(
            MenuManager menuManager, 
            int courseId,
            string courseName)
        {
            return new MenuOptions()
                .AddOption("1", "Students", async () =>
                {
                    await menuManager.ShowCourseStudents(courseId, courseName);
                    return false;
                })
                .AddOption("2", "Announcements", async () =>
                {
                    await menuManager.ShowCourseAnnouncements(courseId);
                    return false;
                })
                .AddOption("3", "Materials", async () =>
                {
                    await menuManager.ShowCourseMaterials(courseId);
                    return false;
                })
                .AddOption("0", "Back", async () => true)
                .Build();
        }
    }
}
