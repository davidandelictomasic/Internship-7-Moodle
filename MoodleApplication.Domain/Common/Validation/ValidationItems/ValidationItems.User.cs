
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Enumumerations.Validations;

namespace MoodleApplication.Domain.Common.Validation.ValidationItems
{
    public class ValidationItems
    {
        public static string CodePrefix = nameof(User);

        public static readonly ValidationItem NameMaxLength = new ValidationItem
        {
            ValidationSeverity = ValidationSeverity.Error,
            ValidationType = ValidationType.FormalValidation,
            Code = $"{CodePrefix}_001",
            Message = $"Name exceeds maximum length({User.NameMaxLength})."
        };
        
        public static readonly ValidationItem EmailUnique = new ValidationItem
        {
            ValidationSeverity = ValidationSeverity.Error,
            ValidationType = ValidationType.BusinessRule,
            Code = $"{CodePrefix}_003",
            Message = $"Email needs to be unique."
        };
        
       
    }
}
