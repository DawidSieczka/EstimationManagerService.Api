using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty()
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLenth)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLenth)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLenth,
                EntityConfigurationValues.DisplayNameMaximumLenth));
        
        RuleFor(x=>x.GroupExternalId).NotEmpty()
            .WithMessage(ValidationMessages.InvalidEmptyValue);
    }
}