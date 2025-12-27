
using MoodleApplication.Domain.Common.Validation;
using MoodleApplication.Domain.Enumumerations.Validations;

namespace MoodleApplication.Application.Common.Model
{
    public class Result<TVaule> where TVaule : class
    {
        private List<ValidationResultItem> _info = new();
        private List<ValidationResultItem> _warnings = new();
        private List<ValidationResultItem> _errors = new();
        public TVaule? Value { get; set; }
        public Guid RequestId { get; set; }
        public bool IsAuthorized { get; set; } = true;

        public IReadOnlyList<ValidationResultItem> Infos
        {
            get => _info.AsReadOnly();
            init => _info.AddRange(value);
        }
        public IReadOnlyList<ValidationResultItem> Errors
        {
            get => _errors.AsReadOnly();
            init => _errors.AddRange(value);
        }
        public IReadOnlyList<ValidationResultItem> Warnings
        {
            get => _warnings.AsReadOnly();
            init => _warnings.AddRange(value);
        }
        public bool HasError => Errors.Any(validationResult => validationResult.ValidationSeverity == ValidationSeverity.Error);
        public bool HasInfo => Infos.Any(validationResult => validationResult.ValidationSeverity == ValidationSeverity.Info);
        public bool HasWarning => Warnings.Any(validationResult => validationResult.ValidationSeverity == ValidationSeverity.Warning);

        public void SetResult(TVaule? value)
        {
            Value = value;
        }
        public void SetValidationResult(ValidationResult validationResult)
        {
            _errors?.AddRange(validationResult.ValidationItems.Where(x => x.ValidationSeverity == ValidationSeverity.Error).Select(x => ValidationResultItem.FromValidationItem(x)));

        }

        public void SetUnaAuthorizedResult() { 


            Value = null;
            IsAuthorized = false;
        }

    }
}
