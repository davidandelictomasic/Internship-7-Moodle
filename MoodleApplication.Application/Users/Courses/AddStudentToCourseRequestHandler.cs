using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Courses
{
    public class AddStudentToCourseRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }

    public class AddStudentToCourseRequestHandler : RequestHandler<AddStudentToCourseRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public AddStudentToCourseRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<SuccessPostResponse>> HandleRequest(
            AddStudentToCourseRequest request,
            Common.Model.Result<SuccessPostResponse> result)
        {
            await _unitOfWork.CourseRepository.AddStudentToCourse(request.CourseId, request.StudentId);

            result.SetResult(new SuccessPostResponse(request.StudentId));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
