using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.User
{
    public class TeachingCourseResponse
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }

    public class GetTeachingCoursesRequest
    {
        public int ProfessorId { get; set; }
    }

    public class GetTeachingCoursesRequestHandler : RequestHandler<GetTeachingCoursesRequest, GetAllResponse<TeachingCourseResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetTeachingCoursesRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<TeachingCourseResponse>>> HandleRequest(
            GetTeachingCoursesRequest request,
            Common.Model.Result<GetAllResponse<TeachingCourseResponse>> result)
        {
            var courses = await _unitOfWork.UserRepository.GetTeachingCourses(request.ProfessorId);

            var courseResponses = courses.Select(c => new TeachingCourseResponse
            {
                CourseId = c.Id,
                CourseName = c.Name
            });

            result.SetResult(new GetAllResponse<TeachingCourseResponse>(courseResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
