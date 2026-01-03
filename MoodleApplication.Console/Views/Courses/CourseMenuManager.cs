using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;
using MoodleApplication.Console.Views.Common;

namespace MoodleApplication.Console.Views.Courses
{
    public class CourseMenuManager
    {
        private readonly CourseActions _courseActions;

        public CourseMenuManager(CourseActions courseActions)
        {
            _courseActions = courseActions;
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

        

        public async Task ShowEditCourseMenu(int courseId, string courseName, int professorId)
        {
            bool exitRequested = false;

            var editCourseMenuOptions = MenuOptions.CreateEditCourseMenuOptions(this, courseId, courseName, professorId);
            while (!exitRequested)
            {
                System.Console.Clear();
                Writer.DisplayMenu($"Moodle - EDIT: {courseName.ToUpper()}", editCourseMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (editCourseMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await editCourseMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task ShowAddStudentMenu(int courseId, string courseName)
        {
            System.Console.Clear();

            var availableStudents = await _courseActions.GetStudentsNotInCourse(courseId);

            if (!availableStudents.Any())
            {
                Writer.WriteMessage("No available students to add (all students are already enrolled).");
                Writer.WaitForKey();
                return;
            }

            bool exitRequested = false;

            var studentListMenuOptions = MenuOptions.CreateAddStudentMenuOptions(this, courseId, courseName, availableStudents);
            while (!exitRequested)
            {
                System.Console.Clear();
                Writer.DisplayMenu($"Moodle - ADD STUDENT TO: {courseName.ToUpper()}", studentListMenuOptions);
                var choice = Reader.ReadMenuChoice();

                if (studentListMenuOptions.ContainsKey(choice))
                {
                    exitRequested = await studentListMenuOptions[choice].Action();
                }
                else
                {
                    System.Console.Clear();
                    Writer.WriteMessage("Invalid option. Please try again.");
                    Writer.WaitForKey();
                }
            }
        }

        public async Task AddStudentToCourse(int courseId, int studentId, string studentName)
        {
            System.Console.Clear();

            var success = await _courseActions.AddStudentToCourse(courseId, studentId);

            if (success)
            {
                Writer.WriteMessage($"Student '{studentName}' added to course successfully!");
            }
            else
            {
                Writer.WriteMessage("Error while adding student to course.");
            }

            Writer.WaitForKey();
        }

        public async Task ShowAddAnnouncementScreen(int courseId, int professorId)
        {
            System.Console.Clear();
            Writer.WriteMessage("=== ADD ANNOUNCEMENT ===\n");

            var title = Reader.ReadString("Title: ");
            var content = Reader.ReadString("Content: ");

            var success = await _courseActions.AddAnnouncement(courseId, professorId, title, content);

            if (success)
            {
                Writer.WriteMessage("\nAnnouncement published successfully!");
            }
            else
            {
                Writer.WriteMessage("\nError publishing announcement.");
            }

            Writer.WaitForKey();
        }

        public async Task ShowAddMaterialScreen(int courseId)
        {
            System.Console.Clear();
            Writer.WriteMessage("=== ADD MATERIAL ===\n");

            var name = Reader.ReadString("Material name: ");
            var url = Reader.ReadUrl("Material URL: ");

            var success = await _courseActions.AddMaterial(courseId, name, url);

            if (success)
            {
                Writer.WriteMessage("\nMaterial added successfully!");
            }
            else
            {
                Writer.WriteMessage("\nError adding material.");
            }

            Writer.WaitForKey();
        }
    }
}
