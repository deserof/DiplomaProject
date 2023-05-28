using FluentValidation;
using Manufacturing.Application.Details.Commands.CreateDetail;

namespace Manufacturing.Application.Details.Commands.UpdateDetail
{
    public class UpdateDetailCommandValidator : AbstractValidator<CreateDetailCommand>
    {
        public UpdateDetailCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}