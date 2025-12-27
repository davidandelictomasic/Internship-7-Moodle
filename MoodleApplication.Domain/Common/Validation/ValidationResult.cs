

using MoodleApplication.Domain.Enumumerations.Validations;

namespace MoodleApplication.Domain.Common.Validation
{
    public class ValidationResult
    {
        private List<ValidationItem> _validationItems = new List<ValidationItem>();
        public IReadOnlyList<ValidationItem> ValidationItems => _validationItems;

        public bool HasError => _validationItems.Any(vi => vi.ValidationSeverity == ValidationSeverity.Error);
        public bool HasInfo => _validationItems.Any(vi => vi.ValidationSeverity == ValidationSeverity.Info);
        public bool HasWarning => _validationItems.Any(vi => vi.ValidationSeverity == ValidationSeverity.Warning);

        public void AddValidationItem(ValidationItem validationItem)
        {
            _validationItems.Add(validationItem);
        }
    }
}
