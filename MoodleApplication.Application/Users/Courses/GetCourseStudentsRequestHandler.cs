using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Courses
{
    public class CourseStudentResponse
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentEmail { get; set; }
    }

    public class GetCourseStudentsRequest
    {
        public int CourseId { get; set; }
    }

    public class GetCourseStudentsRequestHandler : RequestHandler<GetCourseStudentsRequest, GetAllResponse<CourseStudentResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetCourseStudentsRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<CourseStudentResponse>>> HandleRequest(
            GetCourseStudentsRequest request,
            Common.Model.Result<GetAllResponse<CourseStudentResponse>> result)
        {
            var students = await _unitOfWork.CourseRepository.GetStudentsForCourse(request.CourseId);

            var studentResponses = students.Select(s => new CourseStudentResponse
            {
                StudentId = s.Id,
                StudentName = s.Name,
                StudentEmail = s.Email
            });

            result.SetResult(new GetAllResponse<CourseStudentResponse>(studentResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
