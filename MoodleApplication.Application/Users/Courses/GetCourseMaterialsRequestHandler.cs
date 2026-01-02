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
    
    public class MaterialsResponse
    {
        public DateTime MaterialAddedAt { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialURL { get; set; }
    }
    public class GetCourseMaterialsRequest
    {
        public required int Id { get; set; }

    }
    public class GetCourseMaterialsRequestHandler : RequestHandler<GetCourseMaterialsRequest, GetAllResponse<MaterialsResponse>>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public GetCourseMaterialsRequestHandler(IUserUnitOfWork userUnitOfWork)
        {
            _unitOfWork = userUnitOfWork;
        }

        protected async override Task<Common.Model.Result<GetAllResponse<MaterialsResponse>>> HandleRequest(GetCourseMaterialsRequest request, Common.Model.Result<GetAllResponse<MaterialsResponse>> result)
        {
            var coursesMaterials = await _unitOfWork.CourseRepository.GetCourseMaterials(request.Id);

            if (coursesMaterials is null)
            {

                return result;
            }

            var materialsList = coursesMaterials.Select(cm => new MaterialsResponse
            {
                MaterialAddedAt = cm.AddedAt,
                MaterialName = cm.Name,
                MaterialURL = cm.Url
            });

            result.SetResult(new GetAllResponse<MaterialsResponse>(materialsList));

            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
