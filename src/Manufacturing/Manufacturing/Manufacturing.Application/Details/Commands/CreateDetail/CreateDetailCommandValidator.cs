using FluentValidation;

namespace Manufacturing.Application.Details.Commands.CreateDetail
{
    public class CreateDetailCommandValidator : AbstractValidator<CreateDetailCommand>
    {
        public CreateDetailCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}