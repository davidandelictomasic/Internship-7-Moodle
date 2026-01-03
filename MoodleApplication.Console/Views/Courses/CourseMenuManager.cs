using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Helpers;

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
    }
}
