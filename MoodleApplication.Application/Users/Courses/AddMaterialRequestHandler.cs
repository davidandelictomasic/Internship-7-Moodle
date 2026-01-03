using MoodleApplication.Application.Common.Model;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Persistence.Users;

namespace MoodleApplication.Application.Users.Courses
{
    public class AddMaterialRequest
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class AddMaterialRequestHandler : RequestHandler<AddMaterialRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public AddMaterialRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Common.Model.Result<SuccessPostResponse>> HandleRequest(
            AddMaterialRequest request,
            Common.Model.Result<SuccessPostResponse> result)
        {
            var material = new Material
            {
                Name = request.Name,
                Url = request.Url
            };

            await _unitOfWork.CourseRepository.AddMaterial(request.CourseId, material);

            result.SetResult(new SuccessPostResponse(material.Id));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
