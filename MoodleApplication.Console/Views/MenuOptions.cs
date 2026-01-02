using MoodleApplication.Application.Users.User;

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
                   
                    System.Console.Clear();
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
    }
}
