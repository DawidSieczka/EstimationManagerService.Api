using EstimationManagerService.Application.Common.Validations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}