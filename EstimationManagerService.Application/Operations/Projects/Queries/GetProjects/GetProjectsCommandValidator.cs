using EstimationManagerService.Application.Common.Validations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Projects.Queries.GetProjects;

public class GetProjectsCommandValidator : AbstractValidator<GetProjectsCommand>
{
    public GetProjectsCommandValidator()
    {
        RuleFor(x => x.GroupExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}