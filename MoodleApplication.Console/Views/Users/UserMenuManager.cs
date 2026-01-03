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
            bool exitRequested = false;

            var studentMenuOptions = MenuOptions.CreateStudentMenuOptions(this, userId);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - STUDENT MENU", studentMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowPrivateChatMenu(int userId)
        {
            await _chatMenu.ShowPrivateChatMenu(userId);
        }

        public async Task ShowUserCourses(int userId)
        {
            var userCourses = await _userActions.GetUserCourses(userId);

            if (!userCourses.Any())
            {
                System.Console.Clear();
                Writer.WriteMessage("No courses found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var coursesMenuOptions = MenuOptions.CreateStudentCoursesMenuOptions(this, userCourses);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - MY COURSES", coursesMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (coursesMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await coursesMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowSelectedCourseMenu(int courseId)
        {
            bool exitRequested = false;

            var studentCourseMenuOptions = MenuOptions.CreateStudentCourseMenuOptions(this, courseId);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - COURSE", studentCourseMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentCourseMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentCourseMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
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
            bool exitRequested = false;

            var professorMenuOptions = MenuOptions.CreateProfessorMenuOptions(this, userId);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - PROFESSOR MENU", professorMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (professorMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await professorMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowProfessorCourses(int professorId)
        {
            var courses = await _userActions.GetTeachingCourses(professorId);

            if (!courses.Any())
            {
                System.Console.Clear();
                Writer.WriteMessage("No courses found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var coursesMenuOptions = MenuOptions.CreateProfessorCoursesMenuOptions(this, courses);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - MY COURSES", coursesMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (coursesMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await coursesMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowProfessorCourseScreen(int courseId, string courseName)
        {
            bool exitRequested = false;

            var courseMenuOptions = MenuOptions.CreateProfessorCourseMenuOptions(this, courseId, courseName);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu($"Moodle - {courseName.ToUpper()}", courseMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (courseMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await courseMenuOptions[choice].Action();
                }
                else
                {
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowCourseStudents(int courseId, string courseName)
        {
            await _courseMenu.ShowCourseStudents(courseId, courseName);
        }


        public async Task ShowEditCoursesMenu(int professorId)
        {
            var courses = await _userActions.GetTeachingCourses(professorId);

            if (!courses.Any())
            {
                System.Console.Clear();
                Writer.WriteMessage("No courses found.");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var editCoursesMenuOptions = MenuOptions.CreateEditCoursesMenuOptions(this, courses, professorId);
            while (!exitRequested)
            {
                System.Console.Clear();

                Writer.DisplayMenu("Moodle - EDIT COURSES", editCoursesMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (editCoursesMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await editCoursesMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();

                }
            }
        }

        public async Task ShowEditCourseScreen(int courseId, string courseName, int professorId)
        {
            await _courseMenu.ShowEditCourseMenu(courseId, courseName, professorId);
        }
    }
}
