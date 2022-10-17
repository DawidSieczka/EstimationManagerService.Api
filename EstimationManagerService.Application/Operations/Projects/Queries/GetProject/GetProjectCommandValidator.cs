using EstimationManagerService.Application.Common.Validations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Projects.Queries.GetProject;

public class GetProjectCommandValidator : AbstractValidator<GetProjectCommand>
{
    public GetProjectCommandValidator()
    {
        RuleFor(x => x.ProjectExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}