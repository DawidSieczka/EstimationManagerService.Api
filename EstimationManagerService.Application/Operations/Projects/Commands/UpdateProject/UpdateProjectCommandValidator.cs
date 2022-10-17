using EstimationManagerService.Application.Common.Validations;
using EstimationManagerService.Domain.EntitiesConfigurations;
using FluentValidation;

namespace EstimationManagerService.Application.Operations.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty()
            .MinimumLength(EntityConfigurationValues.DisplayNameMinimumLenth)
            .MaximumLength(EntityConfigurationValues.DisplayNameMaximumLenth)
            .WithMessage(ValidationMessages.InvalidLengthValue(EntityConfigurationValues.DisplayNameMinimumLenth,
                EntityConfigurationValues.DisplayNameMaximumLenth));
    }
}