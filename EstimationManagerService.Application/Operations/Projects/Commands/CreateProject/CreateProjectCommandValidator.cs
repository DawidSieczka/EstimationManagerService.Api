using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty()
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLength)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLength)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLength,
                EntityConfigurationValues.DisplayNameMaximumLength));
        
        RuleFor(x=>x.GroupExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}