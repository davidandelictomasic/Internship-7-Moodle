using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Courses
{
    public class AvailableStudentResponse
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentEmail { get; set; }
    }

    public class GetStudentsNotInCourseRequest
    {
        public int CourseId { get; set; }
    }

    public class GetStudentsNotInCourseRequestHandler : RequestHandler<GetStudentsNotInCourseRequest, GetAllResponse<AvailableStudentResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetStudentsNotInCourseRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<GetAllResponse<AvailableStudentResponse>>> HandleRequest(
            GetStudentsNotInCourseRequest request,
            Common.Model.Result<GetAllResponse<AvailableStudentResponse>> result)
        {
            var students = await _unitOfWork.CourseRepository.GetStudentsNotInCourse(request.CourseId);

            var studentResponses = students.Select(s => new AvailableStudentResponse
            {
                StudentId = s.Id,
                StudentName = s.Name,
                StudentEmail = s.Email
            });

            result.SetResult(new GetAllResponse<AvailableStudentResponse>(studentResponses));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
