using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Courses
{
    public class AddAnnouncementRequest
    {
        public int CourseId { get; set; }
        public int ProfessorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class AddAnnouncementRequestHandler : RequestHandler<AddAnnouncementRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public AddAnnouncementRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<SuccessPostResponse>> HandleRequest(
            AddAnnouncementRequest request,
            Common.Model.Result<SuccessPostResponse> result)
        {
            var announcement = new Announcement
            {
                ProfessorId = request.ProfessorId,
                Title = request.Title,
                Content = request.Content
            };

            await _unitOfWork.CourseRepository.AddAnnouncement(request.CourseId, announcement);

            result.SetResult(new SuccessPostResponse(announcement.Id));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
