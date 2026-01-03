using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;
using MoodleApplication.Console.Views.Chats;
using MoodleApplication.Console.Views.Common;
using MoodleApplication.Console.Views.Courses;

namespace MoodleApplication.Console.Views.Users
{
    public class UserMenuManager
    {
        private readonly UserActions _userActions;
        private readonly ChatMenuManager _chatMenu;
        private readonly CourseMenuManager _courseMenu;

        public UserMenuManager(UserActions userActions, ChatMenuManager chatMenu, CourseMenuManager courseMenu)
        {
            _userActions = userActions;
            _chatMenu = chatMenu;
            _courseMenu = courseMenu;
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

        public async Task ShowPrivateChatMenu(int userId)
        {
            await _chatMenu.ShowPrivateChatMenu(userId);
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

        public async Task ShowSelectedCourseMenu(int courseId)
        {
            System.Console.Clear();
            bool exitRequested = false;

            var studentCourseMenuOptions = MenuOptions.CreateStudentCourseMenuOptions(this, courseId);
            while (!exitRequested)
            {
                Writer.DisplayMenu("Moodle - COURSE", studentCourseMenuOptions);
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

        public async Task ShowCourseAnnouncements(int courseId)
        {
            await _courseMenu.ShowCourseAnnouncements(courseId);
        }

        public async Task ShowCourseMaterials(int courseId)
        {
            await _courseMenu.ShowCourseMaterials(courseId);
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
            await _courseMenu.ShowCourseStudents(courseId, courseName);
        }
    }
}
