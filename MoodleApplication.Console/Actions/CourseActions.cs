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

        public CourseActions(
            GetCourseMaterialsRequestHandler getCourseMaterialsRequestHandler, 
            GetCourseAnnouncementsRequestHandler getCourseAnnouncementsRequestHandler, 
            GetCourseStudentsRequestHandler getCourseStudentsRequestHandler
            )
        {
            _getCourseMaterialsRequestHandler = getCourseMaterialsRequestHandler;
            _getCourseAnnouncementsRequestHandler = getCourseAnnouncementsRequestHandler;
            _getCourseStudentsRequestHandler = getCourseStudentsRequestHandler;
        }
        public async Task<IEnumerable<MaterialsResponse>> GetCourseMaterials(int courseId)
        {
            var request = new GetCourseMaterialsRequest
            {
                Id = courseId
            };
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
            var request = new GetCourseAnnouncementsRequest
            {
                Id = courseId
            };
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
    }
}
