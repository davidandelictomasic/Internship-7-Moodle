using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Application.Users.Courses;
using MoodleApplication.Application.Users.User;

namespace MoodleApplication.Console.Actions
{
    public class CourseActions
    {
        private readonly GetCourseMaterialsRequestHandler _getCourseMaterialsRequestHandler;
        private readonly GetCourseAnnouncementsRequestHandler _getCourseAnnouncementsRequestHandler;
        private readonly GetCourseStudentsRequestHandler _getCourseStudentsRequestHandler;
        private readonly GetStudentsNotInCourseRequestHandler _getStudentsNotInCourseRequestHandler;
        private readonly AddStudentToCourseRequestHandler _addStudentToCourseRequestHandler;
        private readonly AddAnnouncementRequestHandler _addAnnouncementRequestHandler;
        private readonly AddMaterialRequestHandler _addMaterialRequestHandler;

        public CourseActions(
            GetCourseMaterialsRequestHandler getCourseMaterialsRequestHandler,
            GetCourseAnnouncementsRequestHandler getCourseAnnouncementsRequestHandler,
            GetCourseStudentsRequestHandler getCourseStudentsRequestHandler,
            GetStudentsNotInCourseRequestHandler getStudentsNotInCourseRequestHandler,
            AddStudentToCourseRequestHandler addStudentToCourseRequestHandler,
            AddAnnouncementRequestHandler addAnnouncementRequestHandler,
            AddMaterialRequestHandler addMaterialRequestHandler)
        {
            _getCourseMaterialsRequestHandler = getCourseMaterialsRequestHandler;
            _getCourseAnnouncementsRequestHandler = getCourseAnnouncementsRequestHandler;
            _getCourseStudentsRequestHandler = getCourseStudentsRequestHandler;
            _getStudentsNotInCourseRequestHandler = getStudentsNotInCourseRequestHandler;
            _addStudentToCourseRequestHandler = addStudentToCourseRequestHandler;
            _addAnnouncementRequestHandler = addAnnouncementRequestHandler;
            _addMaterialRequestHandler = addMaterialRequestHandler;
        }

        public async Task<IEnumerable<MaterialsResponse>> GetCourseMaterials(int courseId)
        {
            var request = new GetCourseMaterialsRequest { Id = courseId };
            var result = await _getCourseMaterialsRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values.Select(cm => new MaterialsResponse
            {
                MaterialAddedAt = cm.MaterialAddedAt,
                MaterialName = cm.MaterialName,
                MaterialURL = cm.MaterialURL
            });
        }

        public async Task<IEnumerable<AnnouncementsResponse>> GetCourseAnnouncements(int courseId)
        {
            var request = new GetCourseAnnouncementsRequest { Id = courseId };
            var result = await _getCourseAnnouncementsRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values.Select(ca => new AnnouncementsResponse
            {
                AnnouncementCreatedAt = ca.AnnouncementCreatedAt,
                AnnouncementTitle = ca.AnnouncementTitle,
                AnnouncementContent = ca.AnnouncementContent,
                ProfessorName = ca.ProfessorName
            });
        }

        public async Task<IEnumerable<CourseStudentResponse>> GetCourseStudents(int courseId)
        {
            var request = new GetCourseStudentsRequest { CourseId = courseId };
            var result = await _getCourseStudentsRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }

        public async Task<IEnumerable<AvailableStudentResponse>> GetStudentsNotInCourse(int courseId)
        {
            var request = new GetStudentsNotInCourseRequest { CourseId = courseId };
            var result = await _getStudentsNotInCourseRequestHandler.ProcessActiveRequestAsnync(request);

            if (result.Value == null)
                return [];

            return result.Value.Values;
        }

        public async Task<bool> AddStudentToCourse(int courseId, int studentId)
        {
            var request = new AddStudentToCourseRequest { CourseId = courseId, StudentId = studentId };
            var result = await _addStudentToCourseRequestHandler.ProcessActiveRequestAsnync(request);

            return result.Value?.Id != null;
        }

        public async Task<bool> AddAnnouncement(int courseId, int professorId, string title, string content)
        {
            var request = new AddAnnouncementRequest
            {
                CourseId = courseId,
                ProfessorId = professorId,
                Title = title,
                Content = content
            };
            var result = await _addAnnouncementRequestHandler.ProcessActiveRequestAsnync(request);

            return result.Value?.Id != null;
        }

        public async Task<bool> AddMaterial(int courseId, string name, string url)
        {
            var request = new AddMaterialRequest
            {
                CourseId = courseId,
                Name = name,
                Url = url
            };
            var result = await _addMaterialRequestHandler.ProcessActiveRequestAsnync(request);

            return result.Value?.Id != null;
        }
    }
}
