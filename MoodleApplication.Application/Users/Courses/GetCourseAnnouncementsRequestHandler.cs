using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Courses
{
   
    public class AnnouncementsResponse
    {
        public DateTime AnnouncementCreatedAt { get; set; }
        public string? AnnouncementTitle { get; set; }
        public string? AnnouncementContent { get; set; }

        public string? ProfessorName { get; set; }
    }
    public class GetCourseAnnouncementsRequest
    {
        public required int Id { get; set; }

    }
    public class GetCourseAnnouncementsRequestHandler : RequestHandler<GetCourseAnnouncementsRequest, GetAllResponse<AnnouncementsResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GetCourseAnnouncementsRequestHandler(IUserUnitOfWork userUnitOfWork)
        {
            _unitOfWork = userUnitOfWork;
        }

        protected async override Task<Common.Model.Result<GetAllResponse<AnnouncementsResponse>>> HandleRequest(GetCourseAnnouncementsRequest request, Common.Model.Result<GetAllResponse<AnnouncementsResponse>> result)
        {
            var courseAnnouncements = await _unitOfWork.CourseRepository.GetCourseAnnouncements(request.Id);

            if (courseAnnouncements is null)
            {

                return result;
            }

            var materialsList = courseAnnouncements.Select(ca => new AnnouncementsResponse
            {
                AnnouncementCreatedAt = ca.CreatedAt,
                AnnouncementTitle = ca.Title,
                AnnouncementContent = ca.Content,
                ProfessorName = ca.Professor.Name
            });

            result.SetResult(new GetAllResponse<AnnouncementsResponse>(materialsList));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
