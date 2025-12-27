

using MoodleApplication.Domain.Common.Validation;
using MoodleApplication.Domain.Enumumerations.Validations;

namespace MoodleApplication.Application.Common.Model
{
    public class ValidationResultItem
    {
        public ValidationSeverity ValidationSeverity { get; init; }
        public ValidationType ValidationType { get; init; }
        public string Code { get; set; }
        public string Message { get; set; }

        public static ValidationResultItem FromValidationItem(ValidationItem item)
        {
            return new ValidationResultItem
            {
                ValidationSeverity = item.ValidationSeverity,
                ValidationType = item.ValidationType,
                Code = item.Code,
                Message = item.Message
            };
        }
    }
}
