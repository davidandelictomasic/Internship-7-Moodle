using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.User
{
    public class CoursesResponse
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
    public class GetUserCoursesRequest
    {
        public required int Id { get; set; }

    }
    public class GetUserCoursesRequestHandler : RequestHandler<GetUserCoursesRequest, GetAllResponse<CoursesResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GetUserCoursesRequestHandler(IUserUnitOfWork userUnitOfWork)
        {
            _unitOfWork = userUnitOfWork;
        }

        protected async override Task<Common.Model.Result<GetAllResponse<CoursesResponse>>> HandleRequest(GetUserCoursesRequest request, Common.Model.Result<GetAllResponse<CoursesResponse>> result)
        {
            var userCourses = await _unitOfWork.UserRepository.GetStudentEnrollments(request.Id);

            if (userCourses is null)
            {

                return result;
            }

            var coursesList = userCourses.Select(uc => new CoursesResponse
            {
                CourseId = uc.Course.Id,
                CourseName = uc.Course.Name
            });

            result.SetResult(new GetAllResponse<CoursesResponse>(coursesList));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
