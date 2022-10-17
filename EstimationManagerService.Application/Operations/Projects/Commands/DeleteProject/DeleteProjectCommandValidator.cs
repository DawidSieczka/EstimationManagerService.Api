using EstimationManagerService.Application.Common.Validations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Projects.Commands.DeleteProject;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.ProjectExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}