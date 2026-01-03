using MoodleApplication.Application.Users.Admin;
using MoodleApplication.Application.Users.Chats;
using MoodleApplication.Application.Users.User;
using MoodleApplication.Console.Views.Admin;
using MoodleApplication.Console.Views.Chats;
using MoodleApplication.Console.Views.Users;
using MoodleApplication.Domain.Enumumerations.Users;

namespace MoodleApplication.Console.Views.Common
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
            return new MenuOptions()
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
                .AddOption("3", "Exit", async () => true)
                .Build();
        }


        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStudentMenuOptions(UserMenuManager userMenu, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "Private chat", async () =>
                {
                    await userMenu.ShowPrivateChatMenu(userId);
                    return false;
                })
                .AddOption("2", "My courses", async () =>
                {
                    await userMenu.ShowUserCourses(userId);
                    return false;
                })
                .AddOption("3", "Log out", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStudentCoursesMenuOptions(UserMenuManager userMenu, IEnumerable<CoursesResponse> courses)
        {
            var menuOptions = new MenuOptions();
            var coursesList = courses.ToList();

            for (int i = 0; i < coursesList.Count; i++)
            {
                var course = coursesList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, course.CourseName ?? "Unnamed Course", async () =>
                {
                    await userMenu.ShowSelectedCourseMenu(course.CourseId);
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateStudentCourseMenuOptions(UserMenuManager userMenu, int courseId)
        {
            return new MenuOptions()
                .AddOption("1", "Announcements", async () =>
                {
                    await userMenu.ShowCourseAnnouncements(courseId);
                    return false;
                })
                .AddOption("2", "Materials", async () =>
                {
                    await userMenu.ShowCourseMaterials(courseId);
                    return false;
                })
                .AddOption("3", "Back", async () => true)
                .Build();
        }


        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateProfessorMenuOptions(UserMenuManager userMenu, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "Private chat", async () =>
                {
                    await userMenu.ShowPrivateChatMenu(userId);
                    return false;
                })
                .AddOption("2", "My courses", async () =>
                {
                    await userMenu.ShowProfessorCourses(userId);
                    return false;
                })
                .AddOption("3", "Log out", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateProfessorCoursesMenuOptions(UserMenuManager userMenu, IEnumerable<TeachingCourseResponse> courses)
        {
            var menuOptions = new MenuOptions();
            var coursesList = courses.ToList();

            for (int i = 0; i < coursesList.Count; i++)
            {
                var course = coursesList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, course.CourseName ?? "Unnamed Course", async () =>
                {
                    await userMenu.ShowProfessorCourseScreen(course.CourseId, course.CourseName ?? "Course");
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateProfessorCourseMenuOptions(UserMenuManager userMenu, int courseId, string courseName)
        {
            return new MenuOptions()
                .AddOption("1", "Students", async () =>
                {
                    await userMenu.ShowCourseStudents(courseId, courseName);
                    return false;
                })
                .AddOption("2", "Announcements", async () =>
                {
                    await userMenu.ShowCourseAnnouncements(courseId);
                    return false;
                })
                .AddOption("3", "Materials", async () =>
                {
                    await userMenu.ShowCourseMaterials(courseId);
                    return false;
                })
                .AddOption("0", "Back", async () => true)
                .Build();
        }


        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreatePrivateChatMenuOptions(ChatMenuManager chatMenu, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "New message", async () =>
                {
                    await chatMenu.ShowNewMessageMenu(userId);
                    return false;
                })
                .AddOption("2", "My chats", async () =>
                {
                    await chatMenu.ShowMyChatRoomsMenu(userId);
                    return false;
                })
                .AddOption("3", "Back", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateUsersListMenuOptions(ChatMenuManager chatMenu, int currentUserId, IEnumerable<UserResponse> users)
        {
            var menuOptions = new MenuOptions();
            var usersList = users.ToList();

            for (int i = 0; i < usersList.Count; i++)
            {
                var user = usersList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, $"{user.Name} ({user.Email})", async () =>
                {
                    await chatMenu.ShowSendMessageScreen(currentUserId, user.UserId, user.Name ?? "Unknown");
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateChatRoomsMenuOptions(ChatMenuManager chatMenu, int currentUserId, IEnumerable<ChatRoomResponse> chatRooms)
        {
            var menuOptions = new MenuOptions();
            var chatRoomsList = chatRooms.ToList();

            for (int i = 0; i < chatRoomsList.Count; i++)
            {
                var chatRoom = chatRoomsList[i];
                var displayNumber = (i + 1).ToString();

                menuOptions.AddOption(displayNumber, chatRoom.OtherUserName ?? "Unknown User", async () =>
                {
                    await chatMenu.ShowChatScreen(currentUserId, chatRoom.ChatRoomId, chatRoom.OtherUserId, chatRoom.OtherUserName ?? "Unknown");
                    return false;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }


        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateAdminMenuOptions(AdminMenuManager adminMenu, int userId)
        {
            return new MenuOptions()
                .AddOption("1", "Private chat", async () =>
                {
                    await adminMenu.ShowPrivateChatMenu(userId);
                    return false;
                })
                .AddOption("2", "Edit Users", async () =>
                {
                    await adminMenu.ShowEditUsersMenu();
                    return false;
                })
                .AddOption("3", "Log out", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateEditUsersMenuOptions(AdminMenuManager adminMenu)
        {
            return new MenuOptions()
                .AddOption("1", "Delete User", async () =>
                {
                    await adminMenu.ShowDeleteUserRoleSelectMenu();
                    return false;
                })
                .AddOption("2", "Edit User Email", async () =>
                {
                    await adminMenu.ShowEditEmailRoleSelectMenu();
                    return false;
                })
                .AddOption("3", "Change Role", async () =>
                {
                    await adminMenu.ShowChangeRoleSelectMenu();
                    return false;
                })
                .AddOption("0", "Back", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateRoleSelectMenuOptions(AdminMenuManager adminMenu, string action)
        {
            return new MenuOptions()
                .AddOption("1", "Student List", async () =>
                {
                    await adminMenu.ShowUserListForAction(UserRole.Student, action);
                    return false;
                })
                .AddOption("2", "Professor List", async () =>
                {
                    await adminMenu.ShowUserListForAction(UserRole.Professor, action);
                    return false;
                })
                .AddOption("0", "Back", async () => true)
                .Build();
        }

        public static Dictionary<string, (string Description, Func<Task<bool>> Action)> CreateUserListForActionMenuOptions(
            AdminMenuManager adminMenu,
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
                    await adminMenu.ExecuteUserAction(user.UserId, user.Name ?? "Unknown", user.Email ?? "", action, currentRole);
                    return true;
                });
            }

            menuOptions.AddOption("0", "Back", async () => true);

            return menuOptions.Build();
        }
    }
}
